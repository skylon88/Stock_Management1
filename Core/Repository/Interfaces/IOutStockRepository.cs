using System;
using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IOutStockHeaderRepository : IGenericRepository<OutStockHeader>
    {
        IList<OutStockHeader> GetAllOutStockHeaders();
        int GetLatestSerialNumber(DateTime month);

    }

    public interface IOutStockRepository : IGenericRepository<OutStock>
    {
        void AddRange(IList<OutStock> entities);
    }

}
