using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using Core.Data;
using Core.Enum;
using Core.Model;
using Core.Repository.Interface;
using OfficeOpenXml;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class RequestService : IRequestService
    {
        private IRequestHeaderRepository _requestHeaderRepository;
        private IRequestRepository _requestRepository;
        private IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RequestService(IUnitOfWork unitOfWork, IRequestHeaderRepository requestHeaderRepository, IRequestRepository requestRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _requestHeaderRepository = requestHeaderRepository;
            _requestRepository = requestRepository;
            _mapper = mapper;
        }
        public IList<RequestViewModel> GetAllByRequestNumber(string Id)
        {
            var result = _requestRepository.GetRequestsByHeaderId(Id);

            //TO DO mapping 
            RequestViewModel[] models = _mapper.Map<RequestViewModel[]>(result);
            return models;
        }


        public bool UpdateRequest(RequestViewModel model)
        {
           
            var requstToUpdate = _requestRepository.GetSignle(model.RequestId);
            requstToUpdate.Reason = model.Reason;
            requstToUpdate.UpdateDate = DateTime.Now;
            try
            {
                _requestRepository.Edit(requstToUpdate);
                _requestRepository.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddRequest(RequestViewModel model)
        {
            Request newInstance = _mapper.Map<Request>(model);
            newInstance.RequestId = Guid.NewGuid();
            try
            {
                _requestRepository.Add(newInstance);
                _requestRepository.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IList<RequestHeaderViewModel> GetAllRequestHeaderByMonth(DateTime? month)
        {
            var result = _requestHeaderRepository.GetAllByMonth(month);

            //TO DO mapping 
            RequestHeaderViewModel[] models = _mapper.Map<RequestHeaderViewModel[]>(result);
            return models;
        }

        public IList<RequestViewModel> GetByRequestCategory(int RequestCategoryId, DateTime? month)
        {
            IList<Request> result = new List<Request>();
            var query = _requestRepository.FindBy(x => x.RequestCategoryId == RequestCategoryId);
            if (month != null)
            {
                query = _requestRepository.FindBy(x => x.RequestCategoryId == RequestCategoryId
                                                    && x.CreateDate.Year == month.Value.Year
                                                    && x.CreateDate.Month == month.Value.Month);
            }
            result = new List<Request>(query);
            //TO DO mapping 
            RequestViewModel[] models = _mapper.Map<RequestViewModel[]>(result);
            return models;
        }


        public int Upload(string fileName, out string message)
        {
            message = string.Empty;
            if (String.IsNullOrEmpty(fileName)) return -2;
            var fi = new FileInfo(fileName);

            using (var package = new ExcelPackage(fi))
            {
                var listOfRequests = new List<Request>();
                var workbook = package.Workbook;
                foreach (var worksheet in workbook.Worksheets)
                {
                    RequestHeader headerObj;
                    IList<Request> readData = ReadRequestRecords(worksheet, out headerObj); //Read Request Records form Excels file
                    listOfRequests.AddRange(readData);

                    _requestHeaderRepository.Add(headerObj);
                    _requestRepository.AddRange(listOfRequests);
                }
                try
                {
                    _requestHeaderRepository.Save();
                    _requestRepository.Save();
                    return 1;
                }
                catch(Exception e)
                {
                    return 0;
                }
            }
        }

        private IList<Request> ReadRequestRecords(ExcelWorksheet worksheet, out RequestHeader requestHeader)
        {
            var listTemplates = new List<Request>();
            var start = worksheet.Dimension.Start;
            var end = worksheet.Dimension.End;
            int startPointToReadRecord = 0;

            var insertHeaderObj = new RequestHeader();
            var listOfRequestRecords = new List<Request>();

            var _ApplicationPerson = worksheet.Cells["F3"].Value?.ToString().Trim(); //需求申请人
            var _ApplicationDept = worksheet.Cells["D3"].Value?.ToString().Trim(); //需求部门
            var _ReportMaker = worksheet.Cells["H3"].Value?.ToString().Trim(); //制表人
            var _FollowUpName = worksheet.Cells["J3"].Value?.ToString().Trim(); //物管跟进人
            var _AuditDept = worksheet.Cells["L3"].Value?.ToString().Trim(); //审核部门
            var _Auditor = worksheet.Cells["N3"].Value?.ToString().Trim(); //审核人
            var _RequestNo = worksheet.Cells["B4"].Value?.ToString().Trim(); //需求单编号
            var _RequestDate = worksheet.Cells["H4"].Value?.ToString().Trim(); // 需求完成日期

            insertHeaderObj.RequestHeaderNumber = GenerateRequestHeaderNumber(_RequestNo);
            insertHeaderObj.CreateDate = DateTime.Now;
            insertHeaderObj.ApplicationPerson = _ApplicationPerson;
            insertHeaderObj.ApplicationDept = _ApplicationDept;
            insertHeaderObj.CreatePerson = _ReportMaker;
            insertHeaderObj.FollowupPerson = _FollowUpName;
            insertHeaderObj.AuditDepart = _AuditDept;
            insertHeaderObj.Auditor = _Auditor;

            for (int row = start.Row; row < end.Row; row++)
            {
                if (worksheet.Cells[row, 1].Value.ToString() == "序号" && worksheet.Cells[row, 2].Value.ToString() == "需求类别")
                {
                    startPointToReadRecord = row + 1;
                    break;
                }

            }

            for (int row = startPointToReadRecord; row < end.Row; row++)
            {
                try
                {
                    var _SerialNo = worksheet.Cells["A" + row].Value?.ToString().Trim(); //序号
                    var _Type = worksheet.Cells["B" + row].Value?.ToString().Trim(); //需求类别
                    //var _ContractNo = worksheet.Cells["C" + row].Value?.ToString().Trim(); //使用合同
                    //var _ContractAddress = worksheet.Cells["D" + row].Value?.ToString().Trim();//合同地址 
                    var _ItemCode = worksheet.Cells["F" + row].Value?.ToString();//物品编码
                    var _TotalCount = worksheet.Cells["G" + row].Value?.ToString().Trim();//数量
                    var _Priority = worksheet.Cells["I" + row].Value?.ToString().Trim();//需求级别
                    var _Reason = worksheet.Cells["J" + row].Value?.ToString().Trim(); //施工材料原因，
                    var _OtherReason = worksheet.Cells["K" + row].Value?.ToString().Trim();//其他需求原因
                    var _Note = worksheet.Cells["O" + row].Value?.ToString().Trim();

                    if (string.IsNullOrEmpty(_Type))
                    {
                        break;
                    }

                    int serialNo;
                    int total;
                    RequestCategoriesEnum type;
                    RequestReason reason;

                    Request insertObj = new Request()
                    {
                        RequestId = Guid.NewGuid(),
                        RequestNumber = insertHeaderObj.RequestHeaderNumber,
                        SerialNumber = Int32.TryParse(_SerialNo, out serialNo) ? serialNo : 0,
                        RequestCategoryId = Enum.TryParse(_Type, out type) ? (int)type : (int)RequestCategoriesEnum.施工材料, //defult is 施工材料
                        //ContractNo = _ContractNo,
                        //ContractAddress = _ContractAddress,
                        Total = Int32.TryParse(_TotalCount, out total) ? total : 0,
                        Priority = _Priority,
                        Reason = Enum.TryParse(_Reason, out reason) ? reason.ToString() : RequestReason.测量.ToString(),
                        OtherReason = _OtherReason,
                        Note = _Note,
                        Status = ProcessStatus.Started
                    };

                    //if (insertHeaderObj.ContractNo == null) // 从需求项目总读取合同号并写入头文件
                    //{
                    //    insertHeaderObj.ContractNo = insertObj.ContractNo;
                    //}

                    listOfRequestRecords.Add(insertObj);
                }
                catch (Exception e)
                {
                    //ServiceHelper.ErrorPrint("需求表", row, 0);
                }
            }
            requestHeader = insertHeaderObj;
            return listOfRequestRecords;

        }

        private string GenerateRequestHeaderNumber(string number)
        {
            string result = string.Empty;
            if(string.IsNullOrEmpty(number))
            {
                int lastSerialNumber = _requestHeaderRepository.GetLatestSerialNumber();
                string nextSerialNumber = ServiceHelper.GenerateCodeNumber("XQ", lastSerialNumber);
                result = nextSerialNumber;
            }
            else
            {
                bool isExisting = _requestHeaderRepository.IsExisting(number);
                if (isExisting)
                {
                    result = number;
                }
                else
                {
                    int lastSerialNumber = _requestHeaderRepository.GetLatestSerialNumber();
                    string nextSerialNumber = ServiceHelper.GenerateCodeNumber("XQ", lastSerialNumber);
                    result = nextSerialNumber;
                }
            }
            return result;
        }

    }
}
