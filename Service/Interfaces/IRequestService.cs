using System;
using System.Collections.Generic;
using System.Text;
using Services.Models;

namespace Services.Interfaces
{
    public interface IRequestService
    {
        IList<RequestViewModel> GetAllByRequestNumber(string Id);
        IList<RequestViewModel> GetByRequestCategory(int RequestCategoryId, DateTime? month = null);
        IList<RequestHeaderViewModel> GetAllRequestHeaderByMonth(DateTime? month);

        bool UpdateRequest(RequestViewModel model);

        bool AddRequest(RequestViewModel model);

        int Upload(string name, out string returnMsg);
    }
}
