using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Model;
using System.Data.Entity;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class RequestHeaderRepository : GenericRepository<RequestHeader>, IRequestHeaderRepository
    {
        public RequestHeaderRepository(UnitOfWork context)
           : base(context)
        {

        }

        public IList<RequestHeader> GetAllByMonth(DateTime? month)
        {
            var result = GetAll().Include(c=>c.Contract).Include(x => x.Requests.Select(i => i.Item.Positions)).ToArray();
            return result;
        }

        public bool IsExisting(string requestHeaderNumber)
        {
            var result = FindBy(x => x.RequestHeaderNumber == requestHeaderNumber).Count();
            if(result > 0)
            {
                return true;
            }
            return false;
        }

        public int GetLatestSerialNumber(DateTime month)
        {
            var latestObj = FindBy(x => x.CreateDate.Year == month.Year && x.CreateDate.Month == month.Month)
                            .OrderByDescending(x => x.SerialNo).FirstOrDefault();
            return latestObj == null ? 0 : latestObj.SerialNo;
        }

        public void AddRange(IList<RequestHeader> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }

        public RequestHeader GetRequestHeader(string requestNumber)
        {
            return FindBy(x => x.RequestHeaderNumber == requestNumber).Include(x=>x.Requests).Include(x => x.Contract).FirstOrDefault();
        }

        public void DeleteRequestHeader(RequestHeader entity)
        {
            Delete(entity);
            Save();
        }
    }
}
