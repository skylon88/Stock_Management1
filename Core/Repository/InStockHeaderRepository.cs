using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    #region IInStockHeaderRepository
    public class InStockHeaderRepository :GenericRepository<InStockHeader>, IInStockHeaderRepository
    {
        public InStockHeaderRepository(UnitOfWork context)
          : base(context)
        {

        }

        public IList<InStockHeader> GetAllInStockHeaders()
        {
            return GetAll().Include(x => x.InStocks.Select(z=>z.Item)).ToList();
        }

        public int GetLatestSerialNumber(DateTime month)
        {
            var latestObj = FindBy(x => x.CreateDate.Year == month.Year && x.CreateDate.Month == month.Month)
                            .OrderByDescending(x => x.SerialNo).FirstOrDefault();
            return latestObj?.SerialNo ?? 0;
        }
    }
    public class InStockRepository : GenericRepository<InStock>, IInStockRepository
    {
        public InStockRepository(UnitOfWork context)
          : base(context)
        {

        }

        public void AddRange(IList<InStock> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }

    }

    #endregion 
}
