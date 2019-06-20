using System.Collections.Generic;
using Core.Enum;
using NewServices;

namespace Test
{
    public class Initiator
    {
        IRequestService _requestService;
        public Initiator(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public IList<RequestViewModel> GetAllData()
        {
            var result = _requestService.GetAllByRequestNumber("1");
            return result;
        }

        public IList<RequestHeaderViewModel> GetAllHeaderData()
        {
            var result = _requestService.GetAllRequestHeaderByMonth(RequestCategoriesEnum.施工材料, null);
            return result;
        }

        public bool UpdateRequest(RequestViewModel model)
        {
            return _requestService.UpdateRequest(model);
        }

        public bool AddRequest(RequestViewModel model)
        {
            return _requestService.AddRequest(model);
        }
    }
}
