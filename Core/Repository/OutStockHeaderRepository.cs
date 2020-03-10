using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    public class OutStockHeaderRepository : GenericRepository<OutStockHeader>, IOutStockHeaderRepository
    {
        public OutStockHeaderRepository(IUnitOfWork context)
          : base(context)
        {

        }

        public IList<OutStockHeader> GetAllOutStockHeaders()
        {
            return GetAll().ToList();
        }

        public int GetLatestSerialNumber(DateTime month)
        {
            var latestObj = FindBy(x => x.CreateDate.Year == month.Year && x.CreateDate.Month == month.Month)
                            .OrderByDescending(x => x.SerialNo).FirstOrDefault();
            return latestObj?.SerialNo ?? 0;
        }
    }

    public class OutStockRepository : GenericRepository<OutStock>, IOutStockRepository
    {
        public OutStockRepository(IUnitOfWork context)
          : base(context)
        {

        }

        public void AddRange(IList<OutStock> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }
    }
}
