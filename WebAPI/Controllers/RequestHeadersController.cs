using Core.Enum;
using NewServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class RequestHeadersController : ApiController
    {
        private readonly IRequestService _requestService;

        public RequestHeadersController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            var data = _requestService.GetAllRequestHeaderByMonth(RequestCategoriesEnum.材料需求);
            return new string[] { "value1", "value2" };
        }
    }
}
