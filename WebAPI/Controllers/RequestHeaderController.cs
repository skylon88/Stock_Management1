using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Enum;
using NewServices.Interfaces;
using NewServices.Models.需求部分;

namespace WebAPI.Controllers
{
    public class RequestHeaderController : ApiController
    {
        private readonly IRequestService _requestService;

        public RequestHeaderController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/requestHeader/{category}
        [ResponseType(typeof(RequestHeaderViewModel))]
        public async Task<IHttpActionResult> Get(int category)
        {
            var data = _requestService.GetAllRequestHeaderByMonth((RequestCategoriesEnum)category);
            return Ok(data);
        }

        //// GET: api/RequestHeader/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/RequestHeader
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RequestHeader/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RequestHeader/5
        public void Delete(int id)
        {
        }
    }
}
