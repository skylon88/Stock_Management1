using System;
using System.Collections.Generic;
using Core.Enum;
using NewServices.Models.需求部分;

namespace NewServices.Interfaces
{
    public interface IRequestService
    {
        IList<RequestHeaderViewModel> GetAllRequestHeaderByCategory(RequestCategoriesEnum requestCategoryId, DateTime? month = null);

        int UpdateRequest(RequestViewModel model);

        bool UpdateRequestHeader(RequestHeaderViewModel model);

        bool DeleteRequest(RequestViewModel model, out string returnMsg);

        int Upload(string name, out string returnMsg);

        void UpdateRequestProcessStatus(Guid requestId, ProcessStatusEnum status);

        bool ExportItemExcel(string path, ReportNameEnum reportType, IList<RequestHeaderViewModel> selectedRequestHeaderViewModel);
    }
}
