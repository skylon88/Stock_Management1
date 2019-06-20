using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {

        }

        public void AddRange(IList<Request> entities)
        {
            foreach(var item in entities)
            {
                Add(item);
            }
        }

        public void DeleteRange(IQueryable<Request> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IList<Request> GetRequestsByHeaderId(string id)
        {
            var query = GetAll().ToList();
            return query;
        }

        public Request GetSingle(Guid id)
        {
            var query = GetAll().Include(x=>x.Item.Positions).FirstOrDefault(x => x.RequestId == id);
            return query;
        }
    }
}
