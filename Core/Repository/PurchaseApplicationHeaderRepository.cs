using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Data;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    #region PurchaseApplicationHeaderRepository
    public class PurchaseApplicationHeaderRepository : GenericRepository<PurchaseApplicationHeader>, IPurchaseApplicationHeaderRepository
    {
        public PurchaseApplicationHeaderRepository(IUnitOfWork context)
          : base(context)
        {

        }

        public IList<PurchaseApplicationHeader> GetAllPurchaseApplicationHeaders()
        {
            return GetAll()
                .Include(x => x.PurchaseApplications.Select(z=>z.Item))
                .Include(x=>x.PurchaseApplications)
                .Include(x=>x.RequestHeader.Contract.PoModel).ToList();
        }

        public int GetLatestSerialNumber(DateTime month)
        {
            var latestObj = FindBy(x=>x.CreateDate.Year == month.Year && x.CreateDate.Month == month.Month)
                            .OrderByDescending(x => x.SerialNo).FirstOrDefault();
            return latestObj?.SerialNo ?? 0;
        }
    }
    #endregion


    #region PurchaseApplicationRepository
    public class PurchaseApplicationRepository : GenericRepository<PurchaseApplication>, IPurchaseApplicationRepository
    {
        public PurchaseApplicationRepository(UnitOfWork context)
           : base(context)
        {

        }

        public void AddRange(IList<PurchaseApplication> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }

        public IList<PurchaseApplication> GetAllPurchaseApplications()
        {
            return GetAll().Include(x => x.PurchaseApplicationHeader.RequestHeader.Contract.PoModel)
                           .Include(x => x.Item).ToList();
        }
    }
    #endregion
}
