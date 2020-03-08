using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Enum;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IRequestHeaderRepository : IGenericRepository<RequestHeader>
    {
        IList<RequestHeader> GetRequestHeadersByCategory(RequestCategoriesEnum requestCategory);

        RequestHeader GetRequestHeader(string requestNumber);

        bool IsExisting(string requestNumber);

        int GetLatestSerialNumber(DateTime month);

        void AddRange(IList<RequestHeader> entities);

        void DeleteRequestHeader(RequestHeader entity);
    }

    public interface IRequestRepository : IGenericRepository<Request>
    {
        Request GetSingle(Guid Id);

        IList<Request> GetRequestsByHeaderId(string Id);

        void AddRange(IList<Request> entities);

        void DeleteRange(IQueryable<Request> entities);
    }

    public interface IFixingRepository : IGenericRepository<FixModel>
    {
    }

}
