using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using AutoMapper;
using Core.Enum;
using Core.Model;
using Core.Repository;
using Core.Repository.Interfaces;
using NewServices.Interfaces;
using NewServices.Models.采购部分;
using NewServices.Models.需求部分;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NewServices.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseApplicationHeaderRepository _purchaseApplicationHeaderRepository;
        private readonly IPurchaseApplicationRepository _purchaseApplicationRepository;
        private readonly IPurchaseHeaderRepository _purchaseHeaderRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IRequestHeaderRepository _requestHeaderRepository;
        private readonly IRequestService _requestService;
        private readonly IManagementService _managementService;
        private readonly IInStockRepository _inStockRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseApplicationHeaderRepository purchaseApplicationHeaderRepository,
                               IPurchaseApplicationRepository purchaseApplicationRepository,
                               IPurchaseHeaderRepository purchaseHeaderRepository,
                               IPurchaseRepository purchaseRepository,
                               IRequestHeaderRepository requestHeaderRepository,
                               IRequestService requestService,
                               IManagementService managementService,
                               IInStockRepository inStockRepository,
                               IMapper mapper)
        {
            _purchaseApplicationHeaderRepository = purchaseApplicationHeaderRepository;
            _purchaseApplicationRepository = purchaseApplicationRepository;
            _purchaseHeaderRepository = purchaseHeaderRepository;
            _purchaseRepository = purchaseRepository;
            _requestHeaderRepository = requestHeaderRepository;
            _requestService = requestService;
            _managementService = managementService;
            _inStockRepository = inStockRepository;
            _mapper = mapper;
        }
        public bool UpdatePurchaseHeader(PurchaseHeaderViewModel model)
        {
            var instanceToUpdate = _purchaseHeaderRepository.FindBy(x => x.PurchaseNumber == model.PurchaseNumber).FirstOrDefault();
            if (instanceToUpdate == null) return false;
            instanceToUpdate.CreatePerson = model.CreatePerson;
            instanceToUpdate.AuditDepart = model.AuditDepart;
            instanceToUpdate.Auditor = model.Auditor;
            instanceToUpdate.DeliveryCategory = model.DeliveryCategory;
            instanceToUpdate.DeliveryDate = model.DeliveryDate;
            instanceToUpdate.PurchaseCategory = model.PurchaseCategory;
            instanceToUpdate.UpdateDate = DateTime.Now;
            try
            {
                _purchaseHeaderRepository.Edit(instanceToUpdate);
                _purchaseHeaderRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CreateApplication(IList<RequestViewModel> requestViewModels, string poNumber, string temp_ApplicationNumber)
        {
            foreach (var requestViewModel in requestViewModels.Where(x => x.ToApplyTotal > 0))
            {
                var currentRequestHeaderNumber = requestViewModel.RequestNumber;
                var purchaseApplicationHeader = _purchaseApplicationHeaderRepository.FindBy(x => x.RequestNumber == currentRequestHeaderNumber).FirstOrDefault();
          
                if (purchaseApplicationHeader == null) //创建一个新的采购申请Header
                {
                    var requestHeader = _requestHeaderRepository.FindBy(x => x.RequestHeaderNumber == currentRequestHeaderNumber).FirstOrDefault();
                    //TO DO mapping 
                    var newPurchaseApplicationHeader = _mapper.Map<PurchaseApplicationHeader>(requestHeader);
                    //Create a new purchase application header.
                    var prefix = newPurchaseApplicationHeader.RequestCategory == RequestCategoriesEnum.采购退货 ? "THSQ" : "SQ";
                    var lastSerialNumber = _purchaseApplicationHeaderRepository.GetLatestSerialNumber(DateTime.Now);
                    newPurchaseApplicationHeader.SerialNo = ++lastSerialNumber;
                    newPurchaseApplicationHeader.PurchaseApplicationNumber = string.IsNullOrEmpty(temp_ApplicationNumber) ? ServiceHelper.GenerateCodeNumber(prefix, newPurchaseApplicationHeader.SerialNo) : temp_ApplicationNumber;
                    _purchaseApplicationHeaderRepository.Add(newPurchaseApplicationHeader);
                    try
                    {
                        _purchaseApplicationHeaderRepository.Save();
                        purchaseApplicationHeader = newPurchaseApplicationHeader;

                        //Update PO Number of contract in RequestHeader.
                        if (!string.IsNullOrEmpty(poNumber))
                        {
                            var poNumberFromDb = _managementService.GetPoNumberIfNotExisting(poNumber);
                            if (requestHeader != null)
                            {
                                requestHeader.Contract.PoId = poNumberFromDb;
                                requestHeader.UpdateDate = DateTime.Now;
                            }

                            _requestHeaderRepository.Save();
                            
                        }
                    }
                    catch (Exception e)
                    {
                        // ignored
                        return;
                    }
                }
                var newPurchaseApplication = _mapper.Map<PurchaseApplication>(requestViewModel);
                newPurchaseApplication.CurrentPurchasePrice = _managementService.GetItemById(requestViewModel.ItemId).Price;
                if (purchaseApplicationHeader != null)
                {
                    newPurchaseApplication.PurchaseApplicationNumber =
                        purchaseApplicationHeader.PurchaseApplicationNumber;
                    newPurchaseApplication.ProcessStatus = ProcessStatusEnum.申请审核中;

                    if (purchaseApplicationHeader.RequestCategory == RequestCategoriesEnum.采购退货)
                    {
                        var latestpurchase = _purchaseRepository
                            .FindBy(x => x.PurchaseApplication.ItemId == requestViewModel.ItemId)
                            .OrderByDescending(x => x.CreateDate).FirstOrDefault();
                        if (latestpurchase != null)
                        {
                            newPurchaseApplication.SupplierId = latestpurchase.PurchaseApplication.SupplierId;
                            newPurchaseApplication.CurrentPurchasePrice = latestpurchase.CurrentPurchasePrice;
                        }
                    }
                }

                _purchaseApplicationRepository.Add(newPurchaseApplication);
                _requestService.UpdateRequestProcessStatus(requestViewModel.RequestId, ProcessStatusEnum.申请审核中);
            }
            _purchaseApplicationRepository.Save();

        }



        public IList<PurchaseApplicationHeaderViewModel> GetAllPurchaseApplicationHeaderViewModels(RequestCategoriesEnum category)
        {
            var listOfPurchaseApplications = _purchaseApplicationHeaderRepository.GetAllPurchaseApplicationHeaders();
            PurchaseApplicationHeaderViewModel[] models = _mapper.Map<PurchaseApplicationHeaderViewModel[]>(listOfPurchaseApplications);
            models = category == RequestCategoriesEnum.采购退货 ? models.Where(x => x.RequestCategory == category).ToArray() : models.Where(x => x.RequestCategory != RequestCategoriesEnum.采购退货).ToArray();

            if (category == RequestCategoriesEnum.材料需求)
            {
                foreach (var item in models)
                {
                    if (item.PurchaseApplicationViewModels.Count() == 0)
                    {
                        item.CompletePercentage = 0;
                    }
                    else
                    {
                        item.CompletePercentage = (decimal)
                            item.PurchaseApplicationViewModels.Count(x => x.ProcessStatus >= ProcessStatusEnum.采购中) /
                            item.PurchaseApplicationViewModels.Count();
                    }
                }
            }
            else
            {
                foreach (var item in models)
                {
                    item.CompletePercentage = (decimal)
                                              item.PurchaseApplicationViewModels.Count(x => x.ProcessStatus >= ProcessStatusEnum.退货中) /
                                              item.PurchaseApplicationViewModels.Count();
                }
            }
            return models;
        }

        public bool UpdateApplication(PurchaseApplicationViewModel model)
        {
            var purchaseApplicationUpdate = _purchaseApplicationRepository.GetAll().FirstOrDefault(x => x.PurchaseApplicationId == model.PurchaseApplicationId);
            if (purchaseApplicationUpdate == null) return false;
            purchaseApplicationUpdate.SerialNumber = model.SerialNumber;
            purchaseApplicationUpdate.TotalConfirmed = model.TotalConfirmed;
            purchaseApplicationUpdate.AuditStatus = model.AuditStatus;
            purchaseApplicationUpdate.Note = model.Note;
            purchaseApplicationUpdate.SupplierId = model.SupplierName;
            purchaseApplicationUpdate.ProcessStatus = model.ProcessStatus;
            purchaseApplicationUpdate.UpdateDate = DateTime.Now;
            if (model.RequestCategory == RequestCategoriesEnum.采购退货)
            {
                purchaseApplicationUpdate.SelectedPurchaseNumber = model.SelectedPurchaseNumber;
                purchaseApplicationUpdate.SupplierId = model.SupplierName;
                purchaseApplicationUpdate.SelectedPONumber = model.PoNumber;
                purchaseApplicationUpdate.CurrentPurchasePrice = model.CurrentPurchasePrice;
            }

            try
            {
                _purchaseApplicationRepository.Edit(purchaseApplicationUpdate);
                _purchaseApplicationRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteApplication(PurchaseApplicationViewModel model)
        {
            var purchaseApplicationToDelete = _purchaseApplicationRepository.FindBy(x => x.PurchaseApplicationId == model.PurchaseApplicationId).FirstOrDefault();
            var isExistingPurchase = _purchaseRepository.FindBy(x => x.PurchaseApplicationId == purchaseApplicationToDelete.PurchaseApplicationId).FirstOrDefault();
            if (isExistingPurchase != null) return false;
            _purchaseApplicationRepository.Delete(purchaseApplicationToDelete);
            _purchaseApplicationRepository.Save();
            if (purchaseApplicationToDelete != null)
                _requestService.UpdateRequestProcessStatus(purchaseApplicationToDelete.RequestId,
                    ProcessStatusEnum.需求建立);
            return true;
        }

        public void CopyApplication(PurchaseApplicationViewModel model)
        {
            var existingPurchaseApplication = _purchaseApplicationRepository.GetAll().FirstOrDefault(x => x.PurchaseApplicationId == model.PurchaseApplicationId);
            var purchaseApplicationCopy = _mapper.Map<PurchaseApplication>(model);
            purchaseApplicationCopy.PurchaseApplicationId = Guid.NewGuid();
            if (existingPurchaseApplication != null)
            {
                purchaseApplicationCopy.PurchaseApplicationNumber = existingPurchaseApplication.PurchaseApplicationNumber;
                purchaseApplicationCopy.SupplierId = existingPurchaseApplication.SupplierId;
            }

            purchaseApplicationCopy.TotalConfirmed = 0;
            purchaseApplicationCopy.AuditStatus = AuditStatusEnum.未审批;
            purchaseApplicationCopy.ProcessStatus = ProcessStatusEnum.申请审核中;
            _purchaseApplicationRepository.Add(purchaseApplicationCopy);
            _purchaseApplicationRepository.Save();
        }

        public IList<PurchaseViewModel> GetAllPurchaseNumberByItemCode(string code = null)
        {
            IList<Purchase> listOfPurchases = string.IsNullOrEmpty(code) ? _purchaseRepository
                .FindBy(x => x.PurchaseHeader.RequestCategory == RequestCategoriesEnum.材料需求)
                .Include(x => x.PurchaseHeader)
                .Include(x => x.PurchaseApplication.PurchaseApplicationHeader.RequestHeader.Contract).ToList() : _purchaseRepository.FindBy(x => x.PurchaseHeader.RequestCategory == RequestCategoriesEnum.材料需求 && x.PurchaseApplication.Item.Code == code).Include(x => x.PurchaseHeader)
                .Include(x => x.PurchaseApplication.PurchaseApplicationHeader.RequestHeader.Contract).ToList();
            var models = _mapper.Map<PurchaseViewModel[]>(listOfPurchases);
            return models;
        }

        //审批采购申请
        public void ConfirmAllApplications(IList<PurchaseApplicationViewModel> models)
        {
            foreach (var item in models)
            {
                var entity = _purchaseApplicationRepository.FindBy(x => x.PurchaseApplicationId == item.PurchaseApplicationId).FirstOrDefault();
                if (entity == null) continue;

                //if (entity.TotalConfirmed == 0)
                //{
                //    entity.TotalConfirmed = entity.TotalApplied;
                //}

                entity.AuditStatus = AuditStatusEnum.已审批;
                _purchaseApplicationRepository.Save();
                //if (entity.PurchaseApplicationHeader.RequestHeader.RequestCategory == RequestCategoriesEnum.采购退货)
                //{
                //    UpdatePurchaseApplicationProcessStatus(entity.PurchaseApplicationId, ProcessStatusEnum.退货中);
                //}
            }
        }

        //生成采购单
        public int CreatePurchase(IList<PurchaseApplicationViewModel> models, string temp_purchaseNumber)
        {
            PurchaseHeader newPurchaseHeader = null;
            IList<Purchase> listOfPurchase = new List<Purchase>();
            models = models.Where(x => x.TotalConfirmed > 0).ToList();
            if (models.Count != 0)
            {
                try
                {
                    foreach (var item in models)
                    {
                        var existingPurchase = _purchaseRepository.FindBy(x => x.PurchaseApplicationId == item.PurchaseApplicationId && x.IsDeleted && item.SupplierName == x.PurchaseHeader.SupplierId).FirstOrDefault();

                        // 重新修改之前的采购单
                        if (existingPurchase != null)
                        {
                            existingPurchase.PurchaseTotal = item.TotalConfirmed;
                            existingPurchase.IsDeleted = false;
                            switch (item.RequestCategory)
                            {
                                case RequestCategoriesEnum.材料需求:
                                    existingPurchase.Status = ProcessStatusEnum.采购中;
                                    break;
                                case RequestCategoriesEnum.采购退货:
                                    existingPurchase.Status = ProcessStatusEnum.退货中;
                                    break;
                            }
                            existingPurchase.UpdateDate = DateTime.Now;
                            _purchaseRepository.Save();
                            switch (item.RequestCategory)
                            {
                                case RequestCategoriesEnum.材料需求:
                                    UpdatePurchaseApplicationProcessStatus(item.PurchaseApplicationId, ProcessStatusEnum.采购中);
                                    break;
                                case RequestCategoriesEnum.采购退货:
                                    UpdatePurchaseApplicationProcessStatus(item.PurchaseApplicationId, ProcessStatusEnum.退货中);
                                    break;
                            }
                            return 1;
                        }
                        else
                        {
                            var purchaseApplicationHeader = _purchaseApplicationHeaderRepository.FindBy(x => x.PurchaseApplicationNumber == item.ApplicationNumber).Include(c => c.RequestHeader).FirstOrDefault();
                            if (newPurchaseHeader == null && purchaseApplicationHeader != null)
                            {
                                newPurchaseHeader = _mapper.Map<PurchaseHeader>(purchaseApplicationHeader);
                                newPurchaseHeader.SupplierId = item.SupplierName;
                                newPurchaseHeader.CreateDate = DateTime.Now;
                                newPurchaseHeader.UpdateDate = DateTime.Now;
                                //Create a new purchase header.
                                var prefix = newPurchaseHeader.RequestCategory == RequestCategoriesEnum.采购退货 ? "RCG" : "CG";
                                var lastSerialNumber = _purchaseHeaderRepository.GetLatestSerialNumber(DateTime.Now);
                                newPurchaseHeader.SerialNo = ++lastSerialNumber;
                                newPurchaseHeader.PurchaseNumber = string.IsNullOrEmpty(temp_purchaseNumber) ? ServiceHelper.GenerateCodeNumber(prefix, newPurchaseHeader.SerialNo) : temp_purchaseNumber;
                                newPurchaseHeader.RequestCategory = purchaseApplicationHeader.RequestHeader.RequestCategory;
                            }

                            if (newPurchaseHeader == null) continue;
                            var newPurchase = new Purchase
                            {
                                PurchaseNumber = newPurchaseHeader.PurchaseNumber,
                                CurrentPurchasePrice = item.CurrentPurchasePrice,
                                PurchaseTotal = item.TotalConfirmed,
                                PurchaseApplicationId = item.PurchaseApplicationId,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                Status = purchaseApplicationHeader != null &&
                                         purchaseApplicationHeader.RequestHeader.RequestCategory ==
                                         RequestCategoriesEnum.采购退货
                                    ? ProcessStatusEnum.退货中
                                    : ProcessStatusEnum.采购中
                            };


                            listOfPurchase.Add(newPurchase);
                        }


                    }
                    _purchaseHeaderRepository.Add(newPurchaseHeader);
                    _purchaseRepository.AddRange(listOfPurchase);
                    _purchaseHeaderRepository.Save();
                    _purchaseRepository.Save();

                    //更新采购申请表的状态和需求表状态
                    foreach (var item in models)
                    {
                        UpdatePurchaseApplicationProcessStatus(item.PurchaseApplicationId,
                            item.RequestCategory == RequestCategoriesEnum.采购退货
                                ? ProcessStatusEnum.退货中
                                : ProcessStatusEnum.采购中);
                    }
                    return 1;
                }
                catch (DbUpdateException)
                {
                    return 0;
                }

            }
            return 0;
        }


        //更新采购单
        public void UpdatePurchase(PurchaseViewModel model)
        {
            var purchase = _purchaseRepository.FindBy(x => x.PurchaseId == model.PurchaseId).Include(z => z.PurchaseApplication.Item).Include(z => z.PurchaseApplication.PurchaseApplicationHeader.RequestHeader).FirstOrDefault();
            if (purchase == null) return;
            purchase.CurrentPurchasePrice = model.Price;
            purchase.ReadyForInStock = model.ReadyForInStock;

            if (Math.Abs(purchase.CurrentPurchasePrice - purchase.PurchaseApplication.Item.Price) > 0.00001)
            {
                purchase.IsPriceChange = true;
            }

            if (purchase.ReadyForInStock == purchase.PurchaseTotal)
            {
                if (purchase.PurchaseApplication.PurchaseApplicationHeader.RequestHeader.RequestCategory ==
                    RequestCategoriesEnum.材料需求)
                {
                    purchase.Status = ProcessStatusEnum.采购完成;
                    purchase.LastDeliveryDate = DateTime.Now;
                }
                else
                {
                    purchase.Status = ProcessStatusEnum.退货申请完成;
                }
            }

            if (model.IsDeleted)
            {
                purchase.IsDeleted = true;
                purchase.PurchaseApplication.ProcessStatus = ProcessStatusEnum.申请审核中;
                purchase.PurchaseApplication.AuditStatus = AuditStatusEnum.未审批;
                purchase.Status = ProcessStatusEnum.申请审核中;
            }

            purchase.Note = model.Note;
            purchase.UpdateDate = DateTime.Now;
            purchase.DeliveryDate = model.DeliveryDate;
            purchase.CorrectionTotal = model.CorrectionTotal;

            _purchaseRepository.Edit(purchase);
            _purchaseRepository.Save();
            UpdatePurchaseProcessStatus(purchase.PurchaseId, purchase.Status);
        }

        //删除采购单
        public void DeletePurchase(PurchaseViewModel model)
        {
            var purchase = _purchaseRepository.FindBy(x => x.PurchaseId == model.PurchaseId).Include(z => z.PurchaseApplication.Item).FirstOrDefault();
            if (purchase == null) return;
            var deletingPurchaseNumber = purchase.PurchaseNumber;
            purchase.PurchaseApplication.ProcessStatus = ProcessStatusEnum.申请审核中;
            purchase.PurchaseApplication.AuditStatus = AuditStatusEnum.未审批;
            purchase.Status = ProcessStatusEnum.申请审核中;

            _purchaseRepository.Delete(purchase);
            _purchaseRepository.Save();

            PurchaseHeader purchaseHeader = _purchaseHeaderRepository.GetPurchaseHeader(deletingPurchaseNumber);
            if (purchaseHeader.Purchases == null || purchaseHeader.Purchases.Count == 0)
            {
                _purchaseHeaderRepository.Delete(purchaseHeader);
                _purchaseHeaderRepository.Save();
            }
        }

        //取得全部采购单
        public IList<PurchaseHeaderViewModel> GetAllPurchaseViewModels(RequestCategoriesEnum category)
        {
            var listOfPurchases = _purchaseHeaderRepository.GetPurchaseHeaders(category).ToList();
            var listOfStocks = _inStockRepository.GetAll();
            var listRequesterHeaders = _requestHeaderRepository.GetAll().Include(x => x.Contract);
            var models = _mapper.Map<PurchaseHeaderViewModel[]>(listOfPurchases);
            foreach (var item in models)
            {
                int numerator = 0;
                int denominator = item.PurchaseViewModels.Count();
                var purchaseApplication = listOfPurchases.FirstOrDefault(x => x.PurchaseNumber == item.PurchaseNumber)?.Purchases.FirstOrDefault()?.PurchaseApplication;
                if (purchaseApplication != null)
                {                  
                    var requestHeaderNumber = purchaseApplication.PurchaseApplicationHeader.RequestNumber;
                    var requestHeader = listRequesterHeaders.FirstOrDefault(x => x.RequestHeaderNumber == requestHeaderNumber);
                    if (category == RequestCategoriesEnum.采购退货)
                    {
                        item.PoNumber = purchaseApplication.SelectedPONumber;
                        numerator = item.PurchaseViewModels.Count(x => x.Status == ProcessStatusEnum.退货申请完成) +
                                      item.PurchaseViewModels.Count(x => x.Status == ProcessStatusEnum.退货出库);
                        
                    }
                    else
                    {
                        var poNumber = requestHeader?.Contract.PoModel.PoNumber;
                        item.PoNumber = poNumber;
                        numerator = item.PurchaseViewModels.Count(x => x.Status == ProcessStatusEnum.采购完成) +
                                      item.PurchaseViewModels.Count(x => x.Status == ProcessStatusEnum.采购入库) +
                                      item.PurchaseViewModels.Count(x => x.Status == ProcessStatusEnum.已出库);               
                    }
                    item.PurchaseViewModels.ToList().ForEach(x => x.PoNumber = item.PoNumber);
                    item.Priority = purchaseApplication.Priority;
                    if (requestHeader != null) item.PurchaseType = requestHeader.RequestCategory;
                }
                item.TotalPrice = item.PurchaseViewModels.Sum(x => x.TotalPrice);
                item.CompletePercentage = (decimal)numerator / denominator;
                item.PurchaseViewModels.ToList()
                    .ForEach(x => x.AlreadyInStock = listOfStocks.Where(l => l.PurchaseId == x.PurchaseId).ToArray().Any() ?
                    listOfStocks.Where(l => l.PurchaseId == x.PurchaseId).Sum(z => z.Total) :
                    0);

            }
            return models;
        }

        //按最大采购数量填充到货数量
        public void FillAllPurchaseTotal(IList<PurchaseViewModel> models)
        {
            foreach (var item in models)
            {
                var purchase = _purchaseRepository.FindBy(x => x.PurchaseId == item.PurchaseId).Include(x => x.PurchaseApplication.PurchaseApplicationHeader.RequestHeader).FirstOrDefault();
                var inStocks = _inStockRepository.FindBy(x => x.PurchaseId == item.PurchaseId).ToArray();
                if (purchase == null) continue;
                {
                    var totalInStock = inStocks.Any() ? inStocks.Sum(x => x.Total) : 0;
                    var purchaseTotal = purchase.PurchaseTotal;
                    purchase.ReadyForInStock = purchaseTotal - totalInStock;
                    purchase.LastDeliveryDate = DateTime.Now;
                    _purchaseRepository.Edit(purchase);
                    _purchaseRepository.Save();

                    //if (purchase.ReadyForInStock == purchase.PurchaseTotal)
                    //{
                    if (purchase.PurchaseApplication.PurchaseApplicationHeader.RequestHeader.RequestCategory == RequestCategoriesEnum.采购退货)
                        {
                            UpdatePurchaseProcessStatus(item.PurchaseId, ProcessStatusEnum.退货申请完成);
                        }
                        else
                        {
                            UpdatePurchaseProcessStatus(item.PurchaseId, ProcessStatusEnum.采购完成);
                            purchase.LastDeliveryDate = DateTime.Now;
                        }

                   // }

                    
                }
            }
        }


        //更新采购状态
        public void UpdatePurchaseProcessStatus(Guid purchaseId, ProcessStatusEnum status)
        {
            Purchase purchase = _purchaseRepository.FindBy(x => x.PurchaseId == purchaseId).FirstOrDefault();
            if (purchase == null) return;
            purchase.Status = status;
            _purchaseRepository.Save();
            UpdatePurchaseApplicationProcessStatus(purchase.PurchaseApplicationId, status);
        }

        //更新采购申请状态
        public void UpdatePurchaseApplicationProcessStatus(Guid purchaseApplicationId, ProcessStatusEnum status)
        {
            var purchaseApplication = _purchaseApplicationRepository.FindBy(x => x.PurchaseApplicationId == purchaseApplicationId).FirstOrDefault();
            if (purchaseApplication == null) return;
            purchaseApplication.ProcessStatus = status;
            purchaseApplication.UpdateDate = DateTime.Now;
            _purchaseApplicationRepository.Save();
            _requestService.UpdateRequestProcessStatus(purchaseApplication.RequestId, status);
        }

        //导出采购申请单
        #region 导出采购申请单
        public bool ExportPurchaseApplicatoin(IList<PurchaseApplicationViewModel> models, string path)
        {
            var excelFile = Resource.采购申请表;
            using (var newpackage = new ExcelPackage())
            {
                IList<string> applicationNumbers = models.Select(x => x.ApplicationNumber).Distinct().ToList();
                foreach (var applicationNumber in applicationNumbers)
                {
                    var list = models.Where(x => x.ApplicationNumber == applicationNumber).ToList();
                    using (var templateStream = new MemoryStream(excelFile))
                    {
                        using (ExcelPackage package = new ExcelPackage(templateStream))
                        {
                            var template = package.Workbook.Worksheets[ReportNameEnum.采购申请单.ToString()];
                            GeneratePurchaseApplicationWorkSheet(list, template);
                            newpackage.Workbook.Worksheets.Add(template.Name, template);
                        }
                    }
                }
                try
                {
                    FileInfo file = new FileInfo(path);
                    newpackage.SaveAs(file);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private static void GeneratePurchaseApplicationWorkSheet(ICollection<PurchaseApplicationViewModel> models,
            ExcelWorksheet template)
        {
            if (models == null || models.Count <= 0) return;
            decimal subTotal = 0;
            var count = 1;
            var row = 7;
            template.Name = models.FirstOrDefault()?.ApplicationNumber;
            template.InsertRow(row, models.Count);

            //Header 
            template.Cells["B3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //制表日期
            template.Cells["B4"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //申请日期
            template.Cells["J4"].Value = models.FirstOrDefault()?.PoNumber; //PO
            template.Cells["L4"].Value = models.FirstOrDefault()?.ContractNo; //合同编号
            template.Cells["H4"].Value = models.FirstOrDefault()?.ApplicationNumber; //申请单号



            //Body
            foreach (var m in models)
            {
                var serialNo = "A" + row;
                var supplierName = "B" + row;
                var type = "C" + row;
                var name = "D" + row;
                var code = "E" + row;
                var brand = "F" + row;
                var model = "G" + row;
                var specification = "H" + row;
                var dimension = "I" + row;
                var totalApplied = "J" + row;
                var totalConfirmed = "K" + row;
                var unit = "L" + row;
                var price = "M" + row;
                var totalPrice = "N" + row;
                var note = "O" + row;

                template.Cells[serialNo].Value = count;
                template.Cells[supplierName].Value = m.SupplierName;
                template.Cells[type].Value = m.Category;
                template.Cells[name].Value = m.Name;
                template.Cells[code].Value = m.Code;
                template.Cells[brand].Value = m.Brand;
                template.Cells[model].Value = m.Model;
                template.Cells[specification].Value = m.Specification;
                template.Cells[dimension].Value = m.Dimension;
                template.Cells[totalApplied].Value = m.TotalApplied;
                template.Cells[totalConfirmed].Value = m.TotalConfirmed;
                template.Cells[unit].Value = m.Unit;
                template.Cells[price].Value = $"{m.CurrentPurchasePrice:C}";
                template.Cells[totalPrice].Value = $"{m.CurrentPurchasePrice * m.TotalConfirmed:C}";
                template.Cells[note].Value = m.Note;

                template.Row(row).Height = 35;
                template.Cells[serialNo + ":" + note].Style.Font.Size = 12;
                template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                subTotal = subTotal + (decimal)m.CurrentPurchasePrice * (decimal)m.TotalConfirmed;
                row++;
                count++;
            }

            template.Cells["N" + row++].Value = $"{subTotal:C}";

            template.Cells["N" + row++].Value = $"{subTotal * (decimal)0.13:C}"; //HST
            template.Cells["N" + row].Value = $"{subTotal * (decimal)1.13:C}"; //总合计
        }
        #endregion

        //导出采购单
        #region 导出采购单

        public bool ExportPurchase(IList<PurchaseViewModel> models, string path, bool isMeger = false)
        {
            var excelFile = Resource.采购申请表;
            using (var newPackage = new ExcelPackage())
            {
                IList<string> purchaseNumbers = models.Select(x => x.PurchaseNumber).Distinct().ToList();
                foreach (var purchaseNumber in purchaseNumbers)
                {
                    var list = models.Where(x => x.PurchaseNumber == purchaseNumber).ToList();
                    if (isMeger)
                    {
                        list = list.GroupBy(x => x.Code).Select(y => new PurchaseViewModel
                        {
                            PurchaseNumber = y.FirstOrDefault().PurchaseNumber,
                            SupplierCode = y.FirstOrDefault().SupplierCode,
                            Status = y.FirstOrDefault().Status,
                            Category = y.FirstOrDefault().Category,
                            Name = y.FirstOrDefault().Name,
                            Code = y.FirstOrDefault().Code,
                            Brand = y.FirstOrDefault().Brand,
                            Model = y.FirstOrDefault().Model,
                            Specification = y.FirstOrDefault().Specification,
                            Dimension = y.FirstOrDefault().Dimension,
                            PurchaseTotal = y.Sum(c=>c.PurchaseTotal),
                            Unit = y.FirstOrDefault().Unit,
                            DefaultPrice = y.FirstOrDefault().DefaultPrice,
                            Price = y.FirstOrDefault().Price,
                            Note = y.FirstOrDefault().Note,
                            TotalPrice = y.Sum(c=>c.TotalPrice)
                        }).ToList();
                    }
                    using (var templateStream = new MemoryStream(excelFile))
                    {
                        using (var package = new ExcelPackage(templateStream))
                        {
                            var template = package.Workbook.Worksheets[ReportNameEnum.采购单.ToString()];
                            GeneratePurchaseWorkSheet(list, template);
                            newPackage.Workbook.Worksheets.Add(template.Name, template);
                        }
                    }
                }
                try
                {
                    var file = new FileInfo(path);
                    newPackage.SaveAs(file);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private void GeneratePurchaseWorkSheet(ICollection<PurchaseViewModel> models, ExcelWorksheet template)
        {
            if (models == null || models.Count <= 0) return;
            decimal subTotal = 0;
            var count = 1;
            var row = 8;
            template.Name = models.FirstOrDefault()?.PurchaseNumber;
            template.InsertRow(row, models.Count);

            //Header 
            var purchaseHeader = _purchaseHeaderRepository.GetPurchaseHeader(models.FirstOrDefault()?.PurchaseNumber);
            var purchaseHeaderViewModel = _mapper.Map<PurchaseHeaderViewModel>(purchaseHeader);
            template.Cells["B3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //出单日期               
            template.Cells["F3"].Value = purchaseHeaderViewModel.ApplicationDept; //制表科室
            template.Cells["I3"].Value = purchaseHeaderViewModel.CreatePerson; //制表人
            template.Cells["L3"].Value = purchaseHeaderViewModel.AuditDepart; //审核部门
            template.Cells["N3"].Value = purchaseHeaderViewModel.AuditDepart; //审核人
            template.Cells["P3"].Value = purchaseHeaderViewModel.CompletePercentage; //完成率
            template.Cells["B4"].Value = purchaseHeaderViewModel.PurchaseNumber; //采购单号
            template.Cells["F4"].Value = purchaseHeaderViewModel.CreatePerson; //采购人
            template.Cells["I4"].Value = purchaseHeaderViewModel.Priority; //采购级别
            template.Cells["L4"].Value = purchaseHeaderViewModel.PurchaseCategory; //采购类型
            template.Cells["I4"].Value = purchaseHeaderViewModel.Priority; //采购级别
            //template.Cells["P4"].Value = purchaseHeaderViewModel.DeliveryDate.Value.ToString("yyyy-MM-dd"); //提货/送货日期
            template.Cells["P5"].Value = models.FirstOrDefault()?.PoNumber; //PONumber
            template.Cells["N5"].Value = purchaseHeaderViewModel.DeliveryCategory; //采购配送类型
            //Body
            var supplierName = "B" + row;
            foreach (var m in models)
            {
                var serialNo = "A" + row;
                var status = "C" + row; //订单号码
                var type = "D" + row;
                var name = "F" + row;
                var code = "G" + row;
                var brand = "H" + row;
                var model = "I" + row;
                var specification = "J" + row;
                var dimension = "K" + row;
                var purchaseTotal = "L" + row;
                var unit = "M" + row;
                var currentPrice = "N" + row;
                var totalPrice = "O" + row;
                var note = "P" + row;

                template.Cells[serialNo].Value = count;
                template.Cells[supplierName].Value = purchaseHeaderViewModel.SupplierName;
                template.Cells[status].Value = m.Status;
                template.Cells[type].Value = m.Category;
                template.Cells[name].Value = m.Name;
                template.Cells[code].Value = m.Code;
                template.Cells[brand].Value = m.Brand;
                template.Cells[model].Value = m.Model;
                template.Cells[specification].Value = m.Specification;
                template.Cells[dimension].Value = m.Dimension;
                template.Cells[purchaseTotal].Value = m.PurchaseTotal;
                template.Cells[unit].Value = m.Unit;
                template.Cells[currentPrice].Value = $"{m.Price:C}";
                template.Cells[totalPrice].Value = $"{m.Price * m.PurchaseTotal:C}";
                template.Cells[note].Value = m.Note;

                template.Row(row).Height = 35;
                template.Cells[serialNo + ":" + note].Style.Font.Size = 12;
                template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                subTotal = subTotal + (decimal)m.Price * (decimal)m.PurchaseTotal;
                row++;
                count++;
            }

            template.Cells["O" + row++].Value = $"{subTotal:C}"; //总计

            template.Cells["O" + row++].Value = $"{subTotal * (decimal)0.13:C}"; //HST
            template.Cells["O" + row].Value = $"{subTotal * (decimal)1.13:C}"; //总合计
        }

        #endregion 
    }
}
