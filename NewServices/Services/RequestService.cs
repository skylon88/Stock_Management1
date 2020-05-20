using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Core.Enum;
using Core.Model;
using Core.Repository;
using Core.Repository.Interfaces;
using NewServices.Interfaces;
using NewServices.Models.管理;
using NewServices.Models.需求部分;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NewServices.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestHeaderRepository _requestHeaderRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IPurchaseApplicationRepository _purchaseApplicationRepository;
        private readonly IFixingRepository _fixingRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IManagementService _managementService;
        private readonly IInStockRepository _inStockRepository;
        private readonly IOutStockRepository _outStockRepository;
        private readonly IContractRepository _contractRepository;

        private readonly IMapper _mapper;

        public RequestService(IRequestHeaderRepository requestHeaderRepository,
                              IPurchaseApplicationRepository purchaseApplicationRepository,
                              IRequestRepository requestRepository,
                              IFixingRepository fixingRepository,
                              IItemRepository itemRepository,
                              IManagementService managementService,
                              IInStockRepository inStockRepository,
                              IOutStockRepository outStockRepository,
                              IContractRepository contractRepository,
                              IMapper mapper)
        {
            _requestHeaderRepository = requestHeaderRepository;
            _purchaseApplicationRepository = purchaseApplicationRepository;
            _requestRepository = requestRepository;
            _fixingRepository = fixingRepository;
            _itemRepository = itemRepository;
            _managementService = managementService;
            _inStockRepository = inStockRepository;
            _outStockRepository = outStockRepository;
            _contractRepository = contractRepository;
            _mapper = mapper;
        }
        public IList<RequestViewModel> GetAllByRequestNumber(string id)
        {
            var result = _requestRepository.GetRequestsByHeaderId(id);

            //TO DO mapping 
            var models = _mapper.Map<RequestViewModel[]>(result);
            return models;
        }
        public bool UpdateRequestHeader(RequestHeaderViewModel model)
        {
            var instanceToUpdate = _requestHeaderRepository.FindBy(x=>x.RequestHeaderNumber == model.RequestHeaderNumber).FirstOrDefault();
            var contract = _contractRepository.FindBy(x=>x.Id == instanceToUpdate.ContractId).FirstOrDefault();
            if (instanceToUpdate == null) return false;
            instanceToUpdate.ApplicationPerson = model.ApplicationPerson;
            instanceToUpdate.CreatePerson = model.CreatePerson;
            instanceToUpdate.FollowupPerson = model.FollowupPerson;
            instanceToUpdate.AuditDepart = model.AuditDepart;
            instanceToUpdate.Auditor = model.Auditor;
            instanceToUpdate.UpdateDate = DateTime.Now;

            contract.Address = model.ContractAddress;
            contract.ContractNumber = model.ContractId;
            try
            {
                _contractRepository.Edit(contract);
                _requestHeaderRepository.Edit(instanceToUpdate);
                _contractRepository.Save();
                _requestHeaderRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int UpdateRequest(RequestViewModel model)
        {
            var result = 0;
            var requstToUpdate = _requestRepository.GetSingle(model.RequestId);
            if(model.RequestCategory == RequestCategoriesEnum.工程车维修 || model.RequestCategory == RequestCategoriesEnum.工具维修)
            {      
                if(requstToUpdate.FixModelId == null)
                {
                    var fixModel = new FixModel()
                    {
                        Id = Guid.NewGuid(),
                        Address = model.FixAddress,
                        Contact = model.Contact,
                        Phone = model.Phone,
                        Price = model.FixingPrice,
                        Days = model.FixingDays,
                        FinishDate = model.FixingFinishDate
                    };
                    _fixingRepository.Add(fixModel);
                    _fixingRepository.Save();
                    requstToUpdate.FixModelId = fixModel.Id;
                }
                else
                {
                    var fixModel = _fixingRepository.FindBy(x => x.Id == requstToUpdate.FixModelId).FirstOrDefault();
                    if (fixModel != null)
                    {
                        fixModel.Address = model.FixAddress;
                        fixModel.Contact = model.Contact;
                        fixModel.Phone = model.Phone;
                        fixModel.Price = model.FixingPrice;
                        fixModel.Days = model.FixingDays;
                        fixModel.FinishDate = model.FixingFinishDate;
                        _fixingRepository.Edit(fixModel);
                    }

                    _fixingRepository.Save();
                }
                
            }       
            if (requstToUpdate.LockStatus == LockStatusEnum.已准备 && model.LockStatus == LockStatusEnum.未准备)
            {
                requstToUpdate.LockTime = null;
            }else
            {
                requstToUpdate.LockTime = DateTime.Now;
            }
            requstToUpdate.LockStatus = model.LockStatus;
            requstToUpdate.Status = model.Status;
            requstToUpdate.Total = model.Total;
            requstToUpdate.PriorityId = (int)model.Priority;
            requstToUpdate.Reason = model.Reason;
            requstToUpdate.OtherReason = model.OtherReason;
            requstToUpdate.Note = model.Note;
            requstToUpdate.UpdateDate = DateTime.Now;
            if (model.PositionName != null || string.IsNullOrEmpty(model.PositionName))
            {
                var position = model.PositionViewModels.FirstOrDefault(x => x.PositionName == model.PositionName);
                if (position != null && requstToUpdate.Item.DefaultPositionName != position.PositionName)
                {
                    requstToUpdate.Item.DefaultPositionName = position.PositionName;
                    result = 2;
                }
            }
            try
            {
                _requestRepository.Edit(requstToUpdate);
                _requestRepository.Save();
                return result;
            }
            catch (Exception)
            {
                result = 0;
                return result;
            }
        }

        public bool DeleteRequestHeader(RequestHeaderViewModel model)
        {
            var requestHeaderToDelete = _requestHeaderRepository.FindBy(x=>x.RequestHeaderNumber == model.RequestHeaderNumber).FirstOrDefault();
            var requestsToDelete = _requestRepository.FindBy(x => x.RequestNumber == model.RequestHeaderNumber);
            try
            {
                _requestRepository.DeleteRange(requestsToDelete);
                _requestHeaderRepository.DeleteRequestHeader(requestHeaderToDelete);
                _requestRepository.Save();
                _requestHeaderRepository.Save();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool DeleteRequest(RequestViewModel model, out string returnMsg)
        {
            var instockCount = _inStockRepository.FindBy(x => x.RequestId == model.RequestId).Count();
            var outstockCount = _outStockRepository.FindBy(x => x.RequestId == model.RequestId).Count();
            var applications = _purchaseApplicationRepository.FindBy(x => x.RequestId == model.RequestId).Count();
            var requestToDelete = _requestRepository.FindBy(x => x.RequestId == model.RequestId).FirstOrDefault();
            try
            {
                //requstToDelete.Status = ProcessStatusEnum.取消;
                //_requestRepository.Edit(requstToDelete);
                if (instockCount == 0 && outstockCount == 0 && applications == 0)
                {
                    _requestRepository.Delete(requestToDelete);
                    _requestRepository.Save();

                    var requestHeader = _requestHeaderRepository.GetRequestHeader(model.RequestNumber);
                    if (requestHeader.Requests == null || requestHeader.Requests.Count == 0)
                    {
                        _requestHeaderRepository.Delete(requestHeader);
                        _requestHeaderRepository.Save();
                    }

                    returnMsg = "删除成功";
                    return true;
                }
                    returnMsg = "此需求项已经有相关的其他操作,无法删除此需求项";
                return false;

            }
            catch (DbUpdateException)
            {
                returnMsg = "删除失败";
                return false;
            }
        }

        public bool AddRequest(RequestViewModel model)
        {
            var newInstance = _mapper.Map<Request>(model);
            newInstance.RequestId = Guid.NewGuid();
            try
            {
                _requestRepository.Add(newInstance);
                _requestRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<RequestHeaderViewModel> GetAllRequestHeaderByCategory(RequestCategoriesEnum requestCategory, DateTime? month)
        {
            if (month == null) month = DateTime.Now;
            var result = _requestHeaderRepository.GetRequestHeadersByCategory(requestCategory);

            //TO DO mapping 
            var requestHeaderViewModels = _mapper.Map<RequestHeaderViewModel[]>(result);
            //var requests = _requestRepository.GetAll().ToList();
            //var requestViewModels = _mapper.Map<RequestViewModel[]>(requests);
            var listOfRequestViewModels = new List<RequestViewModel>();
            foreach (var item in requestHeaderViewModels)
            {
                listOfRequestViewModels.AddRange(item.RequestViewModels);
            }
            var requestViewModels = listOfRequestViewModels.ToArray();
            var listOfOutStocks = _outStockRepository.FindBy(x => x.Type == requestCategory).ToArray();
            var listOfInStocks = _inStockRepository.FindBy(x => x.Type == requestCategory).ToArray();
            
            foreach (var model in requestHeaderViewModels)
            {
                decimal denominator = 1;
                decimal numerator = 1;
                switch (requestCategory)
                {
                    case RequestCategoriesEnum.材料需求:
                        foreach (var item in model.RequestViewModels)
                        {
                             if (item.Status != ProcessStatusEnum.需求建立)
                            {
                                item.ToApplyTotal = 0;
                            }

                            item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.采购入库).Sum(s => s.Total);
                            item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.已出库).Sum(s => s.Total);
                            item.ToOutStockTotal = (item.Total - item.OutStockTotal) - GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks) > 0 ? GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks) : item.Total - item.OutStockTotal;

                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                            item.ToApplyTotal = item.Total - item.AvailableInStorage > 0 ? item.Total - item.AvailableInStorage : 0;

                        }
                        numerator = model.RequestViewModels.Count(x => x.OutStockTotal == x.Total);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = denominator == 0 ? 0 : (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.购入需求:
                        foreach (var item in model.RequestViewModels)
                        {
                            if (item.Status != ProcessStatusEnum.需求建立)
                            {
                                item.ToApplyTotal = 0;
                            }

                            item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.采购入库).Sum(s => s.Total);
                            item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.已出库).Sum(s => s.Total);
                            item.ToOutStockTotal = (item.Total - item.OutStockTotal) - GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks) > 0 ? GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks) : item.Total - item.OutStockTotal;

                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                            //item.ToApplyTotal = item.Total - item.AvailableInStorage > 0 ? item.Total - item.AvailableInStorage : 0;
                            //因为购入需求时不需要计算可采购申请数量
                            item.ToApplyTotal = item.Total;
                        }
                        numerator = model.RequestViewModels.Count(x => x.InStockTotal == x.Total);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = denominator == 0 ? 0 : (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.工具借出:                        
                        foreach (var item in model.RequestViewModels)
                        {
                            if (item.Status == ProcessStatusEnum.需求建立)
                            {
                                item.ToOutStockTotal = GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks);
                            }
                            else
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.归还入库).Sum(s => s.Total);
                                item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.借出出库).Sum(s => s.Total) - item.InStockTotal;
                                item.ToInStockTotal = item.OutStockTotal;
                            }
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = ((decimal)model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.借出出库) / 2) + (decimal)model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.归还入库);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.工程车维修:
                        foreach (var item in model.RequestViewModels)
                        {
                            if (item.Status == ProcessStatusEnum.需求建立)
                            {
                                item.ToInStockTotal = item.Total;
                            }
                            if (item.Status == ProcessStatusEnum.工程车维修入库)
                            {
                                item.OutStockTotal = 0;
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.工程车维修入库).Sum(s => s.Total);
                                item.DestoriedTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.报废出库).Sum(s => s.Total);
                                item.ToOutStockTotal = item.InStockTotal - item.DestoriedTotal;
                            }
                            else if (item.Status == ProcessStatusEnum.维修中)
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                item.DestoriedTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.报废出库).Sum(s => s.Total);
                                item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修中).Sum(s => s.Total) - item.InStockTotal;
                                item.ToInStockTotal = item.OutStockTotal;

                            }
                            else if (item.Status == ProcessStatusEnum.维修完成)
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                item.DestoriedTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.报废出库).Sum(s => s.Total);
                                item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.工程车维修出库).Sum(s => s.Total);
                                item.ToOutStockTotal = item.Total -item.DestoriedTotal;
                            }
                            else if (item.Status == ProcessStatusEnum.工程车维修出库)
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                item.DestoriedTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.报废出库).Sum(s => s.Total);
                                item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.工程车维修出库).Sum(s => s.Total);
                                item.ToInStockTotal = 0;
                                item.ToOutStockTotal = 0;
                                item.ToDestoryTotal = 0;
                            }
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.工程车维修出库) + model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.报废出库);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.工具维修:
                        foreach (var item in model.RequestViewModels)
                        {
                            if (item.Status == ProcessStatusEnum.需求建立)
                            {
                                item.ToOutStockTotal = GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks);
                            }
                            else if (item.Status == ProcessStatusEnum.维修中)
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                item.DestoriedTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.报废出库).Sum(s => s.Total);
                                item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修中).Sum(s => s.Total) - item.InStockTotal;
                                item.ToInStockTotal = item.OutStockTotal;

                            }
                            else if (item.Status == ProcessStatusEnum.维修完成)
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.维修完成).Sum(s => s.Total);
                                item.DestoriedTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.报废出库).Sum(s => s.Total);
                                item.ToOutStockTotal = 0;
                                item.OutStockTotal = 0;
                            }
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.维修完成) + model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.报废出库);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.员工补给:
                        foreach (var item in model.RequestViewModels)
                        {
                           // if (item.Status == ProcessStatusEnum.需求建立 || item.Status == ProcessStatusEnum.采购入库)
                           // {
                                item.ToOutStockTotal = GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks);
                           // }
                           // else
                           // {
                                item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.补给出库).Sum(s => s.Total);
                           // }
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = model.RequestViewModels.Count(x => x.OutStockTotal == x.Total);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.工程车补给:
                        foreach (var item in model.RequestViewModels)
                        {
                            item.ToOutStockTotal = GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks);
                            item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.补给出库).Sum(s => s.Total);
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = model.RequestViewModels.Count(x => x.OutStockTotal >= x.Total);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.物品退回:
                        foreach (var item in model.RequestViewModels)
                        {
                            if (item.Status == ProcessStatusEnum.需求建立)
                            {
                                item.ToInStockTotal = item.Total;
                            }
                            else
                            {
                                item.InStockTotal = listOfInStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.采购入库).Sum(s => s.Total);
                            }
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.退回入库);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.采购退货:
                        foreach (var item in model.RequestViewModels)
                        {
                            if (item.Status == ProcessStatusEnum.需求建立)
                            {
                                item.ToApplyTotal = GetAvailableTotalAmount(item, requestViewModels, listOfOutStocks);
                            }
                            else if(item.Status == ProcessStatusEnum.退货申请完成)
                            {
                                item.ToOutStockTotal = item.Total;
                            }
                            item.OutStockTotal = listOfOutStocks.Where(x => x.RequestId == item.RequestId && x.Status == ProcessStatusEnum.退货出库).Sum(s => s.Total);
                            item.ToOutStockTotal = item.ToOutStockTotal > 0 ? item.ToOutStockTotal : 0;
                            item.ToInStockTotal = item.ToInStockTotal > 0 ? item.ToInStockTotal : 0;
                            item.ToDestoryTotal = item.ToDestoryTotal > 0 ? item.ToDestoryTotal : 0;
                            item.AvailableInStorage = GetAvailableInStorage(item, requestViewModels, listOfOutStocks);
                        }
                        numerator = model.RequestViewModels.Count(x => x.Status == ProcessStatusEnum.退货出库);
                        denominator = model.RequestViewModels.Count();
                        model.CompletePercentage = (decimal)numerator / denominator;
                        break;
                    case RequestCategoriesEnum.办公用品:
                        break;
                }
            }
            return requestHeaderViewModels;
        }

        public IList<RequestViewModel> GetByRequestCategory(RequestCategoriesEnum requestCategory, DateTime? month)
        {
            if (month == null) month = DateTime.Now;
            var query = _requestRepository.FindBy(x => x.RequestHeader.RequestCategory == requestCategory
                                                   && x.CreateDate.Year == month.Value.Year
                                                   && x.CreateDate.Month == month.Value.Month);
            IList<Request> result = new List<Request>(query);
            //TO DO mapping 
            var models = _mapper.Map<RequestViewModel[]>(result);
            return models;
        }


        public int Upload(string fileName, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(fileName)) return -2;
            var fi = new FileInfo(fileName);

            using (var package = new ExcelPackage(fi))
            {
                var workbook = package.Workbook;
                var listOfUnitModel = _managementService.GetUnitModels();
                foreach (var worksheet in workbook.Worksheets)
                {
                    var readData = ReadRequestRecords(listOfUnitModel, worksheet, out var headerObj, out message); //Read Request Records form Excels file
                    if (readData == null || readData.Count == 0)
                    {
                        if (message == string.Empty)
                        {
                            message = "需求表内容不能为空";
                        }
                        return 0;
                    }
                    try
                    {
                        _requestHeaderRepository.Add(headerObj);
                        _requestRepository.AddRange(readData);
                        _requestHeaderRepository.Save();
                        _requestRepository.Save();
                    }
                    catch (DbUpdateException e)
                    {
                        if (e.InnerException == null) return 0;
                        if (e.InnerException.InnerException is SqlException msgException) message = msgException.Message;

                        return 0;
                    }
                }
                return !string.IsNullOrEmpty(message) ? 3 : 1;
            }
        }

        private IList<Request> ReadRequestRecords(IList<UnitModel> listOfUnitModels, ExcelWorksheet worksheet, out RequestHeader requestHeader, out string errorMessage)
        {
            var insertHeaderObj = new RequestHeader();
            var listOfRequestRecords = new List<Request>();
            var start = worksheet.Dimension.Start;
            var end = worksheet.Dimension.End;
            int startPointToReadRecord = 0;
            errorMessage = string.Empty;

            try
            {
                var applicationPerson = worksheet.Cells["F3"].Value?.ToString().Trim(); //需求申请人
                var applicationDept = worksheet.Cells["D3"].Value?.ToString().Trim(); //需求部门
                var reportMaker = worksheet.Cells["H3"].Value?.ToString().Trim(); //制表人
                var followUpName = worksheet.Cells["J3"].Value?.ToString().Trim(); //物管跟进人
                var auditDept = worksheet.Cells["L3"].Value?.ToString().Trim(); //审核部门
                var auditor = worksheet.Cells["N3"].Value?.ToString().Trim(); //审核人
                var requestNo = worksheet.Cells["B4"].Value?.ToString().Trim(); //需求单编号


                insertHeaderObj.RequestHeaderNumber = GenerateRequestHeaderNumber(requestNo, out var serialNoInRequestHeader);
                insertHeaderObj.SerialNo = serialNoInRequestHeader;
                insertHeaderObj.CreateDate = DateTime.Now;
                insertHeaderObj.ApplicationPerson = applicationPerson;
                insertHeaderObj.ApplicationDept = applicationDept;
                insertHeaderObj.CreatePerson = reportMaker;
                insertHeaderObj.FollowupPerson = followUpName;
                insertHeaderObj.AuditDepart = auditDept;
                insertHeaderObj.Auditor = auditor;

                for (var row = start.Row; row < end.Row; row++)
                {
                    if (worksheet.Cells[row, 1].Value.ToString() != "序号" ||
                        worksheet.Cells[row, 2].Value.ToString() != "需求类别") continue;
                    startPointToReadRecord = row + 1;
                    break;
                }

                for (var row = startPointToReadRecord; row < end.Row; row++)
                {

                    var serialNo = worksheet.Cells["A" + row].Value?.ToString().Trim(); //序号
                    var type = worksheet.Cells["B" + row].Value?.ToString().Trim(); //需求类别
                    var contractNo = worksheet.Cells["C" + row].Value?.ToString().Trim(); //使用合同
                    var contractAddress = worksheet.Cells["D" + row].Value?.ToString().Trim();//合同地址 
                    var itemCode = worksheet.Cells["F" + row].Value?.ToString();//物品编码
                    var totalCount = worksheet.Cells["G" + row].Value?.ToString().Trim();//数量
                    var unit = worksheet.Cells["H" + row].Value?.ToString().Trim();//Unit
                    var priority = worksheet.Cells["I" + row].Value?.ToString().Trim();//需求级别
                    var reason = worksheet.Cells["J" + row].Value?.ToString().Trim(); //施工材料原因
                    var otherReason = worksheet.Cells["K" + row].Value?.ToString().Trim();//其他需求原因
                    var note = worksheet.Cells["O" + row].Value?.ToString().Trim();
                    var item = GetItemId(itemCode);

                    if (string.IsNullOrEmpty(type)) break;
                    if (item == null || item.ItemId == Guid.Empty)
                    {
                        errorMessage = errorMessage + itemCode + " & ";
                        continue;
                    }

                    if (insertHeaderObj.ContractId == Guid.Empty)
                    {
                        //TO DO 
                        //Insert/Select a ContractId from DB
                        var existingContract = _managementService.GetContractByContractId(contractNo);
                        if (existingContract != null)
                        {
                            insertHeaderObj.ContractId = existingContract.Id;
                        }
                        else
                        {
                            var newContractInstance = new ContractViewModel()
                            {
                                Id = Guid.NewGuid(),
                                ContractNumber = contractNo,
                                Address = contractAddress,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now
                            };
                            _managementService.CreateContract(newContractInstance);
                            insertHeaderObj.ContractId = newContractInstance.Id;
                        }
                    }

                    insertHeaderObj.RequestCategory = Enum.TryParse(type, out RequestCategoriesEnum requestCategories) ? requestCategories : RequestCategoriesEnum.材料需求; //defult is 施工材料


                    var isNotZero = double.TryParse(totalCount, out var total) ? total : 0;
                    if (isNotZero <= 0) continue;
                    var insertObj = new Request()
                    {
                        RequestId = Guid.NewGuid(),
                        RequestNumber = insertHeaderObj.RequestHeaderNumber,
                        SerialNumber = int.TryParse(serialNo, out var serialNumber) ? serialNumber : 0,
                        ItemId = item.ItemId,
                        Total = double.TryParse(totalCount, out total) ? total : 0,
                        Reason = Enum.TryParse(reason, out RequestReason requestReason) ? requestReason.ToString() : RequestReason.测量.ToString(),
                        OtherReason = otherReason,
                        Note = note,
                        Status = ProcessStatusEnum.需求建立
                    };

                    //单位转换
                    if (!item.Unit.Contains(unit))
                    {
                        if (listOfUnitModels.FirstOrDefault(x => x.ItemName == itemCode) != null)
                        {
                            var unitConversion = listOfUnitModels.FirstOrDefault(x => x.ItemName == itemCode);
                            if (unit.Contains(unitConversion.ConvertToUnit))
                            {
                                var result = decimal.Round((decimal)(insertObj.Total / unitConversion.Factor), 2, MidpointRounding.AwayFromZero);
                                insertObj.Total = (double)result;
                                insertObj.Note += "单位已转换";
                            }
                            else
                            {
                                errorMessage = errorMessage + itemCode + " 转换单位不存在 & ";
                                continue;
                            }
                        }
                
                    }

                    SetPriority(insertObj, priority);
                    listOfRequestRecords.Add(insertObj);


                }

                if(errorMessage != string.Empty) {
                    requestHeader = null;
                    return null;
                }

                requestHeader = insertHeaderObj;
                return listOfRequestRecords;
            }
            catch (Exception)
            {
                requestHeader = null;
                return null;
                //ServiceHelper.ErrorPrint("需求表", row, 0);
            }

        }

        private string GenerateRequestHeaderNumber(string number, out int serialNo)
        {
            string result;
            serialNo = 0;
            if (string.IsNullOrEmpty(number))
            {
                var lastSerialNumber = _requestHeaderRepository.GetLatestSerialNumber(DateTime.Now);
                serialNo = ++lastSerialNumber;
                var nextSerialNumber = ServiceHelper.GenerateCodeNumber("XQ", serialNo);
                result = nextSerialNumber;
            }
            else
            {
                result = number;
                /*
                var isExisting = _requestHeaderRepository.IsExisting(number);
                if (isExisting)
                {
                    result = number;
                }
                else
                {

                    
                    var lastSerialNumber = _requestHeaderRepository.GetLatestSerialNumber(DateTime.Now);
                    serialNo = ++lastSerialNumber;
                    var nextSerialNumber = ServiceHelper.GenerateCodeNumber("XQ", serialNo);
                    result = nextSerialNumber;
                    
                }
            */
            }
            return result;
        }

        private Item GetItemId(string code)
        {
            var result = _itemRepository.FindBy(x => x.Code == code).FirstOrDefault();
            return result;
        }

        private void SetPriority(Request request,string priority)
        {
            switch (priority)
            {
                case "3级":
                    request.PriorityId = 3;
                    break;
                case "2级":
                    request.PriorityId = 2;
                    break;
                case "1级":
                    request.PriorityId = 1;
                    break;
                default:
                    request.PriorityId = 1;
                    break;
            }
        }


        public void UpdateRequestProcessStatus(Guid requestId, ProcessStatusEnum status)
        {
            var request = _requestRepository.FindBy(x => x.RequestId == requestId).FirstOrDefault();
            if (request != null)
            {
                request.Status = status;
                request.UpdateDate = DateTime.Now;
            }

            _requestRepository.Save();
        }

        private double GetAvailableInStorage(RequestViewModel model, RequestViewModel[] requestViewModels, OutStock[] listOfOutStocks)
        {
            var max = model.Max; //库存上限;
            var currentTotalItemInStorage = model.PositionViewModels.Sum(x => x.Total); //该物品全部库位上库存数总和.
            double holdingStock = 0;
            //处理锁定状态
            var allRestRequests = requestViewModels
                .Where(x => x.ItemId == model.ItemId &&
                x.RequestId != model.RequestId &&
                x.LockStatus == LockStatusEnum.已准备 &&
                x.Status != ProcessStatusEnum.已出库 &&
                x.Status != ProcessStatusEnum.报废出库 &&
                x.Status != ProcessStatusEnum.补给出库 &&
                x.Status != ProcessStatusEnum.退货出库 &&
                //以下条件解决购入需求时避免item锁定
                x.RequestCategory == RequestCategoriesEnum.购入需求 && x.Status != ProcessStatusEnum.采购入库).ToList();
            if (model.LockStatus == LockStatusEnum.已准备)
            {
                allRestRequests = allRestRequests
                .Where(x => x.LockTime < model.LockTime).ToList();
            }

            IList<RequestViewModel> allRestRequestViewModels = new List<RequestViewModel>();

            //if (model.RequestCategory == RequestCategoriesEnum.材料需求)
            //{

            //    allRestRequestViewModels = allRestRequests.Where(x => x.Status != ProcessStatusEnum.已出库
            //                                                              || (x.Status == ProcessStatusEnum.已出库 &&
            //                                                                  x.Total != listOfOutStocks
            //                                                                      .Where(y =>
            //                                                                          y.RequestId == x.RequestId &&
            //                                                                          x.Status == ProcessStatusEnum.已出库)
            //                                                                      .Sum(s => s.Total))).ToList();
            //}
            //if (model.RequestCategory == RequestCategoriesEnum.采购退货)
            //{
            //    allRestRequestViewModels = allRestRequests.Where(x => x.Status != ProcessStatusEnum.退货出库
            //                                                          || (x.Status == ProcessStatusEnum.退货出库 &&
            //                                                              x.Total != listOfOutStocks
            //                                                                  .Where(y =>
            //                                                                      y.RequestId == x.RequestId &&
            //                                                                      x.Status == ProcessStatusEnum.退货出库)
            //                                                                  .Sum(s => s.Total))).ToList();
            //}
            //else
            //{
            //    allRestRequestViewModels = allRestRequests;
            //}

            if (allRestRequests.Count != 0)

            {
                holdingStock = allRestRequests.Sum(t => t.Total);
                currentTotalItemInStorage = currentTotalItemInStorage - holdingStock;
            }

            var selectedTotalItemInStorage = model.PositionViewModels.FirstOrDefault(x => x.PositionName == model.PositionName)?.Total; //已选的库位上的库存量;
            var totalRequest = model.Total; //需求数量;

            // //考虑库存上限
            //var avaiableStorage = (currentTotalItemInStorage - max) - selectedTotalItemInStorage > 0
            //    ? selectedTotalItemInStorage
            //    : (currentTotalItemInStorage - max) > 0 ? currentTotalItemInStorage - max : 0;

            //不考虑库存上限
            var avaiableStorage = currentTotalItemInStorage - selectedTotalItemInStorage > 0
                ? selectedTotalItemInStorage
                : currentTotalItemInStorage > 0 ? currentTotalItemInStorage : 0;
            return (double)avaiableStorage;
        }

        private double GetAvailableTotalAmount(RequestViewModel model, RequestViewModel[] requestViewModels, OutStock[] listOfOutStocks)
        {
            var avaiableStorage = GetAvailableInStorage(model, requestViewModels, listOfOutStocks);

            var result = (model.PositionViewModels != null && model.Total - avaiableStorage > 0 ?
                avaiableStorage :
                model.Total);
            return result;
        }

        public bool ExportItemExcel(string path, ReportNameEnum reportType, IList<RequestHeaderViewModel> selectedRequestHeaderViewModel)
        {
            byte[] excelFile = Resource.采购申请表;
            using (ExcelPackage newPackage = new ExcelPackage())
            {
                using (var templateStream = new MemoryStream(excelFile))
                {
                    using (ExcelPackage package = new ExcelPackage(templateStream))
                    {
                        var template = package.Workbook.Worksheets[ReportNameEnum.捡货.ToString()];
                        GenerateItemCheckSheet(template, selectedRequestHeaderViewModel);
                        newPackage.Workbook.Worksheets.Add(template.Name, template);
                    }
                }
                try
                {
                    FileInfo file = new FileInfo(path);
                    newPackage.SaveAs(file);
                    return true;
                }
                catch (Exception)
                {
                    return false;}
            }
        }

        private void GenerateItemCheckSheet(ExcelWorksheet template, IList<RequestHeaderViewModel> selectedRequestHeaderViewModel)
        {
            var row = 7;
            template.Name = "捡货";

            var listOfRequestHeaders = _requestHeaderRepository.GetAll().Include(x => x.Requests.Select(i => i.Item.Positions)).Include(x => x.Contract).ToList();
            var listOfItems = new List<Item>();
            //Header 
            template.Cells["B3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //出单日期   
            template.Cells["D3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //制表日期   
            //template.Cells["D3"].Value = outstockHeaderViewModel.ApplicationDept; //制表科室
            //template.Cells["F3"].Value = outstockHeaderViewModel.CreatePerson; //制表人

            //Dyanmic header
            if (selectedRequestHeaderViewModel.Count > 0)
            {
                var requestHeaderColumnStart = 13;
                var requestHeaderRowStart = 5;
                var requestHeaderContractRowStart = 6;
                foreach (var requestHeader in selectedRequestHeaderViewModel)
                {
                    template.Cells[requestHeaderRowStart, requestHeaderColumnStart, requestHeaderRowStart, requestHeaderColumnStart + 1].Value = requestHeader.RequestHeaderNumber + "  " + requestHeader.ContractId + "  " + requestHeader.ContractAddress;
                    template.Cells[requestHeaderRowStart, requestHeaderColumnStart, requestHeaderRowStart, requestHeaderColumnStart + 1].Merge = true;
                    template.Cells[requestHeaderRowStart, requestHeaderColumnStart, requestHeaderRowStart, requestHeaderColumnStart + 1].Style.WrapText = true;
                    template.Cells[requestHeaderContractRowStart, requestHeaderColumnStart].Value = "需求数量";
                    template.Cells[requestHeaderContractRowStart, requestHeaderColumnStart + 1].Value = "捡货数量";

                    template.Cells[requestHeaderRowStart, requestHeaderColumnStart, requestHeaderRowStart, requestHeaderColumnStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    template.Cells[requestHeaderRowStart, requestHeaderColumnStart, requestHeaderRowStart, requestHeaderColumnStart + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    template.Cells[requestHeaderContractRowStart, requestHeaderColumnStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    template.Cells[requestHeaderContractRowStart, requestHeaderColumnStart].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    template.Cells[requestHeaderContractRowStart, requestHeaderColumnStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    template.Cells[requestHeaderContractRowStart, requestHeaderColumnStart + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    template.Column(requestHeaderColumnStart).Width = 16;
                    template.Column(requestHeaderColumnStart + 1).Width = 16;

                    requestHeaderColumnStart = requestHeaderColumnStart + 2;

                    var requests = listOfRequestHeaders.FirstOrDefault(x => x.RequestHeaderNumber == requestHeader.RequestHeaderNumber)?.Requests;
                    if (requests == null) continue;
                    foreach (var request in requests)
                    {
                        if (!listOfItems.Contains(request.Item))
                        {
                            listOfItems.Add(request.Item);
                        }
                    }
                }
            }


            //Body
            foreach (var m in listOfItems)
            {
                StringBuilder comboOfPosition = new StringBuilder();
                foreach (var p in m.Positions)
                {

                    comboOfPosition.AppendLine(p.PositionName + "(" + p.Total + ")");
                }
                    var serialNo = "A" + row;
                    var category = "B" + row; //材料类别
                    var projectCategory = "F" + row; //所属专项类
                    var chinese = "C" + row; //材料名称
                    var position = "D" + row; //库位
                    var code = "E" + row;
                    var model = "F" + row;
                    var specification = "G" + row;
                    var dimension = "H" + row;
                    var unit = "I" + row;
                    var note = "X" + row;
                    var ArrangeOrder = "J" + row; //摆放顺序
                    var ArrangePosition = "K" + row; //摆放位置
                    //var TotalBudget = "L" + row; //材料总预算

                var requestHeaderColumnStart = 13;
                foreach(var requestHeader in selectedRequestHeaderViewModel)
                {
                    var totalRequest = requestHeader.RequestViewModels.Where(x => x.ItemId == m.ItemId).Sum(i => i.Total);
                    template.Cells[row, requestHeaderColumnStart].Value = totalRequest;
                    requestHeaderColumnStart = requestHeaderColumnStart + 2;
                }


                    template.Cells[serialNo].Value = serialNo;

                    template.Cells[category].Value = m.Category;template.Cells[projectCategory].Value = m.ProjectCategory;
                    template.Cells[chinese].Value = m.ChineseName;
                    template.Cells[code].Value = m.Code;
                    template.Cells[model].Value = m.Model;
                    template.Cells[specification].Value = m.Specification;
                    template.Cells[dimension].Value = m.Dimension;
                    //template.Cells[LastInstockTotal].Value = model.Total;
                    template.Cells[position].Value = comboOfPosition;
                    template.Cells[unit].Value = m.Unit;
                    //template.Cells[Diffy].Value = string.Format("{0:C}", model.Price);
                    template.Cells[ArrangeOrder].Value = m.ArrangeOrder;
                    template.Cells[ArrangePosition].Value = m.ArrangePosition;
                    template.Cells[note].Value = m.Comments;

                    template.Row(row).Height = 30;
                    template.Cells[serialNo + ":" + note].Style.Font.Size = 10;
                    template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    row++;
                //}

            }
        }
    }
}
