using System;
using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IInStockHeaderRepository : IGenericRepository<InStockHeader>
    {
        int GetLatestSerialNumber(DateTime month);

        IList<InStockHeader> GetAllInStockHeaders();

    }
    public interface IInStockRepository : IGenericRepository<InStock>
    {
        void AddRange(IList<InStock> entities);
    }
}
