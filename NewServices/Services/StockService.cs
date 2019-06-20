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
using NewServices.Models.入库部分;
using NewServices.Models.出库部分;
using NewServices.Models.采购部分;
using NewServices.Models.需求部分;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NewServices.Services
{
    public class StockService : IStockService
    {
        private readonly IManagementService _managementService;
        private readonly IRequestHeaderRepository _requestHeaderRepository;
        private readonly IPurchaseService _purchaseService;
        private readonly IRequestService _requestService;
        private readonly IPurchaseHeaderRepository _purchaseHeaderRepository;
        private readonly IInStockHeaderRepository _inStockHeaderRepository;
        private readonly IInStockRepository _inStockRepository;
        private readonly IOutStockHeaderRepository _outStockHeaderRepository;
        private readonly IOutStockRepository _outStockRepository;
        private readonly IMapper _mapper;

        public StockService(IManagementService managementService,
                            IRequestHeaderRepository requestHeaderRepository,
                            IPurchaseService purchaseService,
                            IRequestService requestService,
                            IPurchaseHeaderRepository purchaseHeaderRepository,
                            IInStockHeaderRepository inStockHeaderRepository,
                            IInStockRepository inStockRepository,
                            IOutStockHeaderRepository outStockHeaderRepository,
                            IOutStockRepository outStockRepository,
                            IMapper mapper)
        {
            _managementService = managementService;
            _requestHeaderRepository = requestHeaderRepository;
            _purchaseService = purchaseService;
            _requestService = requestService;
            _purchaseHeaderRepository = purchaseHeaderRepository;
            _inStockHeaderRepository = inStockHeaderRepository;
            _inStockRepository = inStockRepository;
            _outStockHeaderRepository = outStockHeaderRepository;
            _outStockRepository = outStockRepository;
            _mapper = mapper;
        }

        //从采购单入库
        public void CreateInStock(IList<PurchaseViewModel> models, string temp_instockNumber)
        {
            if (models != null && models.Count > 0)
            {
                IList<InStock> listOfInStocks = new List<InStock>();
                PurchaseHeader purchaseHeader = _purchaseHeaderRepository.GetPurchaseHeader(models.FirstOrDefault()?.PurchaseNumber);
                InStockHeader newInStockHeader = _mapper.Map<InStockHeader>(purchaseHeader);
                int lastSerialNumber = _inStockHeaderRepository.GetLatestSerialNumber(DateTime.Now);
                newInStockHeader.SerialNo = ++lastSerialNumber;
                newInStockHeader.InStockNumber = string.IsNullOrEmpty(temp_instockNumber) ? ServiceHelper.GenerateCodeNumber("RK", newInStockHeader.SerialNo) : temp_instockNumber;


                try
                {
                    _inStockHeaderRepository.Add(newInStockHeader);

                    foreach (PurchaseViewModel model in models)
                    {
                        InStock newInstock = _mapper.Map<InStock>(model);
                        newInstock.Total = (model.PurchaseTotal - model.AlreadyInStock) >= model.ReadyForInStock ? model.ReadyForInStock : model.PurchaseTotal - model.AlreadyInStock;
                        newInstock.Type = purchaseHeader.RequestCategory;
                        newInstock.InStockNumber = newInStockHeader.InStockNumber;
                        newInstock.Status = ProcessStatusEnum.采购入库;
                        newInstock.PositionName = "Stage";
                        if (newInstock.Total <= 0) continue;

                        listOfInStocks.Add(newInstock);

                        if (newInstock.Total + model.AlreadyInStock == model.PurchaseTotal)
                        {
                            _purchaseService.UpdatePurchaseProcessStatus(model.PurchaseId, ProcessStatusEnum.采购入库);
                        }
                        else
                        {
                            _purchaseService.UpdatePurchaseProcessStatus(model.PurchaseId, ProcessStatusEnum.采购中);
                        }
                        _managementService.UpdateStorage(model.Code, model.Position, newInstock.Total, newInstock.InStockNumber);
                        _managementService.UpdateItemPrice(model.ItemId, model.Price);
                    }
                    if (listOfInStocks.Count > 0)
                    {
                        _inStockRepository.AddRange(listOfInStocks);
                        _inStockHeaderRepository.Save();
                        _inStockRepository.Save();
                    }

                }
                catch (DbUpdateException)
                {
                }
            }
        }

        //从需求单入库
        public void CreateInStock(IList<RequestViewModel> models, string temp_instockNumber)
        {
            if (models == null || models.Count <= 0) return;
            RequestHeader requestHeader = _requestHeaderRepository.GetRequestHeader(models.FirstOrDefault()?.RequestNumber);
            InStockHeader newInStockHeader = _mapper.Map<InStockHeader>(requestHeader);
            int lastSerialNumber = _inStockHeaderRepository.GetLatestSerialNumber(DateTime.Now);
            newInStockHeader.SerialNo = ++lastSerialNumber;
            newInStockHeader.InStockNumber = string.IsNullOrEmpty(temp_instockNumber) ? ServiceHelper.GenerateCodeNumber("RK", newInStockHeader.SerialNo) : temp_instockNumber;

            var listOfOutStocksExisting = _outStockRepository.FindBy(x => x.Type == requestHeader.RequestCategory).ToArray();
            var listOfInStocksExisting = _inStockRepository.FindBy(x => x.Type == requestHeader.RequestCategory).ToArray();

            try
            {
                _inStockHeaderRepository.Add(newInStockHeader);
                _inStockHeaderRepository.Save();
                foreach (RequestViewModel model in models)
                {
                    var newInStock = _mapper.Map<InStock>(model);
                    newInStock.Type = model.RequestCategory;
                    newInStock.InStockNumber = newInStockHeader.InStockNumber;
                    switch (model.RequestCategory)
                    {
                        case RequestCategoriesEnum.工程车维修 when model.Status == ProcessStatusEnum.需求建立:
                            newInStock.Status = ProcessStatusEnum.工程车维修入库;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.工程车维修入库);
                            break;
                        case RequestCategoriesEnum.工程车维修:
                            {
                                if (model.Status == ProcessStatusEnum.维修中)
                                {
                                    var alreadyOutStocks = listOfOutStocksExisting.Where(x => x.RequestId == model.RequestId && x.Status == ProcessStatusEnum.维修中).Sum(s => s.Total);
                                    var alreadyInStocks = listOfInStocksExisting.Where(x => x.RequestId == model.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                    newInStock.Status = ProcessStatusEnum.维修完成;
                                    if (alreadyInStocks + newInStock.Total == alreadyOutStocks)
                                    {
                                        _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.维修完成);
                                    }
                                }

                                break;
                            }
                        case RequestCategoriesEnum.工具维修:
                            {
                                var alreadyOutStocks = listOfOutStocksExisting.Where(x => x.RequestId == model.RequestId && x.Status == ProcessStatusEnum.维修中).Sum(s => s.Total);
                                var alreadyInStocks = listOfInStocksExisting.Where(x => x.RequestId == model.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                newInStock.Status = ProcessStatusEnum.维修完成;
                                if (alreadyInStocks + newInStock.Total == alreadyOutStocks)
                                {
                                    _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.维修完成);
                                }

                                break;
                            }
                        case RequestCategoriesEnum.工具借出:
                            {
                                var alreadyOutStocks = listOfOutStocksExisting.Where(x => x.RequestId == model.RequestId && x.Status == ProcessStatusEnum.借出出库).Sum(s => s.Total);
                                var alreadyInStocks = listOfInStocksExisting.Where(x => x.RequestId == model.RequestId && x.Status == ProcessStatusEnum.归还入库).Sum(s => s.Total);
                                newInStock.Status = ProcessStatusEnum.归还入库;
                                if (alreadyInStocks + newInStock.Total == alreadyOutStocks)
                                {
                                    _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.归还入库);
                                }

                                break;
                            }
                        case RequestCategoriesEnum.物品退回:
                            newInStock.Status = ProcessStatusEnum.退回入库;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.退回入库);
                            break;
                    }

                    _inStockRepository.Add(newInStock);
                    _inStockRepository.Save();
                    _managementService.UpdateStorage(model.Code, "Stage", newInStock.Total, newInStock.InStockNumber);

                }

            }
            catch (DbUpdateException)
            {
            }

        }
        public void CreateOutStock(IList<RequestViewModel> models, string temp_outStockNumber)
        {
            if (models == null || models.Count <= 0) return;
            RequestHeader requestHeader = _requestHeaderRepository.GetRequestHeader(models.FirstOrDefault()?.RequestNumber);
            OutStockHeader newOutStockHeader = _mapper.Map<OutStockHeader>(requestHeader);
            var allPurchases = _purchaseService.GetAllPurchaseNumberByItemCode();
            int lastSerialNumber = _outStockHeaderRepository.GetLatestSerialNumber(DateTime.Now);
            newOutStockHeader.SerialNo = ++lastSerialNumber;
            newOutStockHeader.OutStockHeaderNumber = string.IsNullOrEmpty(temp_outStockNumber) ? ServiceHelper.GenerateCodeNumber("CK", newOutStockHeader.SerialNo) : temp_outStockNumber;
            newOutStockHeader.RequestNumber = requestHeader.RequestHeaderNumber;
            try
            {
                _outStockHeaderRepository.Add(newOutStockHeader);
                _outStockHeaderRepository.Save();
                foreach (RequestViewModel model in models)
                {
                    OutStock newOutStock = _mapper.Map<OutStock>(model);
                    newOutStock.Type = model.RequestCategory;
                    newOutStock.OutStockNumber = newOutStockHeader.OutStockHeaderNumber;
                    var selectedPurchase = allPurchases.Where(x => x.RequestId == model.RequestId).FirstOrDefault();
                    if (selectedPurchase !=null)
                    {
                        newOutStock.Price = selectedPurchase.Price;
                    }

                    switch (model.RequestCategory)
                    {
                        //报废出库
                        case RequestCategoriesEnum.工程车维修 when model.ToDestroy:
                            newOutStock.Status = ProcessStatusEnum.报废出库;
                            break;
                        //非报废出库
                        case RequestCategoriesEnum.工程车维修:
                            switch (model.Status)
                            {
                                case ProcessStatusEnum.工程车维修入库:
                                    newOutStock.Status = ProcessStatusEnum.维修中;
                                    _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.维修中);
                                    break;
                                case ProcessStatusEnum.维修完成:
                                    newOutStock.Status = ProcessStatusEnum.工程车维修出库;
                                    _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.工程车维修出库);
                                    break;
                            }

                            break;
                        case RequestCategoriesEnum.工具维修 when model.ToDestroy: //报废出库
                            newOutStock.Status = ProcessStatusEnum.报废出库;
                            break;
                        case RequestCategoriesEnum.工具维修:
                            newOutStock.Status = ProcessStatusEnum.维修中;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.维修中);
                            break;
                        case RequestCategoriesEnum.工具借出:
                            newOutStock.Status = ProcessStatusEnum.借出出库;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.借出出库);
                            break;
                        case RequestCategoriesEnum.工程车补给:
                        case RequestCategoriesEnum.员工补给:
                            newOutStock.Status = ProcessStatusEnum.补给出库;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.补给出库);
                            break;
                        case RequestCategoriesEnum.采购退货:
                            newOutStock.Status = ProcessStatusEnum.退货出库;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.退货出库);
                            break;
                        default:
                            newOutStock.Status = ProcessStatusEnum.已出库;
                            _requestService.UpdateRequestProcessStatus(model.RequestId, ProcessStatusEnum.已出库);
                            break;
                    }

                    _outStockRepository.Add(newOutStock);
                    _outStockRepository.Save();
                    _managementService.UpdateStorage(model.Code, model.PositionName, -newOutStock.Total, newOutStock.OutStockNumber);
                }

            }
            catch (DbUpdateException)
            {
            }
        }

        public IList<InStockHeaderViewModel> GetAllInStockHeaderViewModel()
        {
            var listOfInStockHeader = _inStockHeaderRepository.GetAllInStockHeaders();
            IList<InStockHeaderViewModel> listOfInStockHeaderViewModels = _mapper.Map<InStockHeaderViewModel[]>(listOfInStockHeader);
            foreach (var inStockHeader in listOfInStockHeaderViewModels)
            {
                var requestHeader = _requestHeaderRepository.GetRequestHeader(inStockHeader.RequestNumber);

                switch (inStockHeader.InStockCategory)
                {
                    case RequestCategoriesEnum.工具维修:
                        inStockHeader.ContractNumber = requestHeader?.Contract?.ContractNumber;
                        break;
                    case RequestCategoriesEnum.工程车维修:
                        {
                            inStockHeader.ContractNumber = requestHeader?.Contract?.ContractNumber;
                            foreach (var inStock in inStockHeader.InStockViewModels)
                            {
                                var request = requestHeader.Requests.FirstOrDefault(x => x.RequestId == inStock.RequestId);
                                if (request != null) inStock.Price = request.Item.Price;
                                inStock.TotalPrice = inStock.Price * inStock.Total;

                            }
                            break;
                        }
                    case RequestCategoriesEnum.工具借出:
                        {
                            inStockHeader.ContractNumber = requestHeader?.Contract?.ContractNumber;
                            foreach (var inStock in inStockHeader.InStockViewModels)
                            {
                                inStock.TotalPrice = 0;
                                inStock.Price = 0;
                            }

                            break;
                        }
                    case RequestCategoriesEnum.物品退回:
                        inStockHeader.ContractNumber = requestHeader?.Contract?.ContractNumber;
                        break;

                    default:
                        {
                            var purchaseHeader = _purchaseHeaderRepository.GetPurchaseHeader(inStockHeader.PurchaseNumber);
                            inStockHeader.PoNumber = requestHeader?.Contract?.PoModel?.PoNumber;
                            inStockHeader.SupplierName = purchaseHeader?.SupplierId;
                            foreach (var instock in inStockHeader.InStockViewModels)
                            {
                                if (!instock.PurchaseId.HasValue) continue;
                                var purchase = purchaseHeader.Purchases.FirstOrDefault(x => x.PurchaseId == instock.PurchaseId);
                                if (purchase == null) continue;
                                instock.Price = purchase.CurrentPurchasePrice;
                                instock.TotalPrice = instock.Price * instock.Total;
                            }

                            break;
                        }
                }
            }
            return listOfInStockHeaderViewModels;
        }

        public IList<OutStockHeaderViewModel> GetAllOutStockHeaderViewModel()
        {
            var listOfOutstockHeader = _outStockHeaderRepository.GetAllOutStockHeaders();
            IList<OutStockHeaderViewModel> listOfOutstockHeaderViewModels = _mapper.Map<OutStockHeaderViewModel[]>(listOfOutstockHeader);
            foreach (var outStockHeader in listOfOutstockHeaderViewModels)
            {
                if (outStockHeader.OutStockCategory == RequestCategoriesEnum.工具维修 ||
                   outStockHeader.OutStockCategory == RequestCategoriesEnum.工程车维修)
                {
                    var requestHeader = _requestHeaderRepository.GetRequestHeader(outStockHeader.RequestNumber);
                    //outStockHeader.ContractNumber = requestHeader?.Contract?.ContractNumber;
                    //outStockHeader.Address = requestHeader?.Contract?.Address;
                    foreach (var outStock in outStockHeader.OutStockViewModels)
                    {
                        var request = requestHeader.Requests.FirstOrDefault(x => x.RequestId == outStock.RequestId);
                        if (request?.FixModel != null)
                        {
                            outStock.TotalPrice = request.FixModel.Price;
                        }
                        else outStock.TotalPrice = 0;
                        outStock.Price = 0;
                    }
                }
            }

            return listOfOutstockHeaderViewModels;
        }

        public bool ExportInStock(IList<InStockHeaderViewModel> models, string path)
        {
            byte[] excelFile = Resource.采购申请表;
            using (ExcelPackage newpackage = new ExcelPackage())
            {
                foreach (var instockheader in models)
                {
                    using (var templateStream = new MemoryStream(excelFile))
                    {
                        using (ExcelPackage package = new ExcelPackage(templateStream))
                        {
                            var template = package.Workbook.Worksheets[ReportNameEnum.入库单.ToString()];
                            GenerateInStockWorkSheet(instockheader, template);
                            try
                            {
                                newpackage.Workbook.Worksheets.Add(template.Name, template);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
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

        private void GenerateInStockWorkSheet(InStockHeaderViewModel inStockHeaderViewModel, ExcelWorksheet template)
        {
            if (inStockHeaderViewModel == null) return;
            decimal subTotal = 0;
            var row = 8;
            template.Name = inStockHeaderViewModel.InStockNumber;
            template.InsertRow(row, inStockHeaderViewModel.InStockViewModels.Count);

            //Header 
            var requestHeader = _requestHeaderRepository.FindBy(x => x.RequestHeaderNumber == inStockHeaderViewModel.RequestNumber).Include(c => c.Contract).FirstOrDefault();
            template.Cells["B3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //出单日期               
            template.Cells["D3"].Value = inStockHeaderViewModel.ApplicationDept; //制表科室
            template.Cells["F3"].Value = inStockHeaderViewModel.CreatePerson; //制表人
            template.Cells["H3"].Value = inStockHeaderViewModel.AuditDepart; //审核部门
            template.Cells["J3"].Value = inStockHeaderViewModel.AuditDepart; //审核人
            template.Cells["B4"].Value = inStockHeaderViewModel.InStockNumber; //入库单号
            template.Cells["D4"].Value = inStockHeaderViewModel.CreatePerson; //仓库名称
            template.Cells["H4"].Value = inStockHeaderViewModel.PurchaseNumber; //采购单号
            if (requestHeader != null)
            {
                template.Cells["F4"].Value = requestHeader.Contract.Address; //合同地址                    
                template.Cells["J4"].Value = requestHeader.Contract.ContractNumber; //合同编号
                template.Cells["B5"].Value = requestHeader.RequestCategory.ToString(); //入库类型
            }

            //Body
            foreach (var m in inStockHeaderViewModel.InStockViewModels)
            {
                var serialNo = "A" + row;
                var name = "B" + row;
                var code = "C" + row;
                var model = "D" + row;
                var specification = "E" + row;
                var dimension = "F" + row;
                var position = "G" + row; //库位
                var total = "H" + row;
                var unit = "I" + row;
                var currentPrice = "J" + row;
                var totalPrice = "K" + row;
                var note = "L" + row;

                template.Cells[serialNo].Value = serialNo;
                template.Cells[name].Value = m.Name;
                template.Cells[code].Value = m.Code;
                template.Cells[model].Value = m.Model;
                template.Cells[specification].Value = m.Specification;
                template.Cells[dimension].Value = m.Dimension;
                template.Cells[position].Value = m.PositionName;
                template.Cells[total].Value = m.Total;
                template.Cells[unit].Value = m.Unit;
                template.Cells[currentPrice].Value = $"{m.Price:C}";
                template.Cells[totalPrice].Value = $"{m.Price * m.Total:C}";
                template.Cells[note].Value = m.Note;

                template.Row(row).Height = 35;
                template.Cells[serialNo + ":" + note].Style.Font.Size = 12;
                template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                subTotal = subTotal + (decimal)m.Price * m.Total;
                row++;
            }

            template.Cells["K" + row++].Value = $"{subTotal:C}"; //总计

            template.Cells["K" + row++].Value = $"{subTotal * (decimal)0.13:C}"; //HST
            template.Cells["K" + row].Value = $"{subTotal * (decimal)1.13:C}"; //总合计
        }

        public bool ExportOutStock(IList<OutStockHeaderViewModel> models, string path)
        {
            byte[] excelFile = Resource.采购申请表;
            using (ExcelPackage newpackage = new ExcelPackage())
            {
                foreach (var outstockheader in models)
                {
                    using (var templateStream = new MemoryStream(excelFile))
                    {
                        using (ExcelPackage package = new ExcelPackage(templateStream))
                        {
                            var template = package.Workbook.Worksheets[ReportNameEnum.出库单.ToString()];
                            GenerateOutStockWorkSheet(outstockheader, template);
                            try
                            {
                                newpackage.Workbook.Worksheets.Add(template.Name, template);
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
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

        private void GenerateOutStockWorkSheet(OutStockHeaderViewModel outStockHeaderViewModel, ExcelWorksheet template)
        {
            if (outStockHeaderViewModel == null) return;
            decimal subTotal = 0;
            var row = 8;
            template.Name = outStockHeaderViewModel.OutStockNumber;
            template.InsertRow(row, outStockHeaderViewModel.OutStockViewModels.Count);

            //Header 
            var requestHeader = _requestHeaderRepository.FindBy(x => x.RequestHeaderNumber == outStockHeaderViewModel.RequestNumber).Include(c => c.Contract).FirstOrDefault();
            template.Cells["B3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //出单日期               
            template.Cells["D3"].Value = outStockHeaderViewModel.ApplicationDept; //制表科室
            template.Cells["F3"].Value = outStockHeaderViewModel.CreatePerson; //制表人
            template.Cells["H3"].Value = outStockHeaderViewModel.AuditDepart; //审核部门
            template.Cells["J3"].Value = outStockHeaderViewModel.AuditDepart; //审核人
            template.Cells["D4"].Value = outStockHeaderViewModel.AuditDepart; //领料人
            template.Cells["B4"].Value = outStockHeaderViewModel.OutStockNumber; //出库单号
            template.Cells["F4"].Value = outStockHeaderViewModel.CreatePerson; //领料小组
            if (requestHeader != null)
            {
                template.Cells["H4"].Value = requestHeader.Contract.Address; //出货地址
                template.Cells["J4"].Value = requestHeader.Contract.ContractNumber; //合同编号
                template.Cells["B5"].Value = requestHeader.RequestCategory.ToString(); //出库类型
            }

            //Body

            foreach (var m in outStockHeaderViewModel.OutStockViewModels)
            {
                var serialNo = "A" + row;
                var name = "B" + row;
                var code = "C" + row;
                var model = "D" + row;
                var specification = "E" + row;
                var dimension = "F" + row;
                var position = "G" + row; //库位
                var total = "H" + row;
                var unit = "I" + row;
                var currentPrice = "J" + row;
                var totalPrice = "K" + row;
                var note = "L" + row;

                template.Cells[serialNo].Value = serialNo;
                template.Cells[name].Value = m.Name;
                template.Cells[code].Value = m.Code;
                template.Cells[model].Value = m.Model;
                template.Cells[specification].Value = m.Specification;
                template.Cells[dimension].Value = m.Dimension;
                template.Cells[position].Value = m.PositionName;
                template.Cells[total].Value = m.Total;
                template.Cells[unit].Value = m.Unit;
                template.Cells[currentPrice].Value = $"{m.Price:C}";
                template.Cells[totalPrice].Value = $"{m.Price * m.Total:C}";
                template.Cells[note].Value = m.Note;

                template.Row(row).Height = 35;
                template.Cells[serialNo + ":" + note].Style.Font.Size = 12;
                template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                subTotal = subTotal + (decimal)m.Price * m.Total;
                row++;
            }

            template.Cells["K" + row++].Value = $"{subTotal:C}"; //总计

            template.Cells["K" + row++].Value = $"{subTotal * (decimal)0.13:C}"; //HST
            template.Cells["K" + row].Value = $"{subTotal * (decimal)1.13:C}"; //总合计
        }



        #region Update Header
        public bool UpdateInStockHeader(InStockHeaderViewModel model)
        {
            var instanceToUpdate = _inStockHeaderRepository.FindBy(x => x.InStockNumber == model.InStockNumber).FirstOrDefault();
            if (instanceToUpdate == null) return false;
            //instanceToUpdate.ApplicationPerson = model.ApplicationPerson;
            instanceToUpdate.CreatePerson = model.CreatePerson;
            //instanceToUpdate.FollowupPerson = model.FollowupPerson;
            instanceToUpdate.AuditDepart = model.AuditDepart;
            instanceToUpdate.Auditor = model.Auditor;
            instanceToUpdate.UpdateDate = DateTime.Now;
            try
            {
                _inStockHeaderRepository.Edit(instanceToUpdate);
                _inStockHeaderRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateOutStockHeader(OutStockHeaderViewModel model)
        {
            var instanceToUpdate = _outStockHeaderRepository.FindBy(x => x.OutStockHeaderNumber == model.OutStockNumber).FirstOrDefault();
            if (instanceToUpdate == null) return false;
            //instanceToUpdate.ApplicationPerson = model.ApplicationPerson;
            instanceToUpdate.CreatePerson = model.CreatePerson;
            //instanceToUpdate.FollowupPerson = model.FollowupPerson;
            instanceToUpdate.AuditDepart = model.AuditDepart;
            instanceToUpdate.Auditor = model.Auditor;
            instanceToUpdate.UpdateDate = DateTime.Now;
            try
            {
                _outStockHeaderRepository.Edit(instanceToUpdate);
                _outStockHeaderRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
