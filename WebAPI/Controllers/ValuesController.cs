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
    public class ValuesController : ApiController
    {
        private readonly IRequestService _requestService;

        public ValuesController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            var data = _requestService.GetAllRequestHeaderByMonth(RequestCategoriesEnum.材料需求);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
