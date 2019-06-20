using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Data;
using Core.Enum;
using Core.Model;
using Core.Repository.Interfaces;

namespace Core.Repository
{
    #region PurchaseHeaderRepository
    public class PurchaseHeaderRepository : GenericRepository<PurchaseHeader>, IPurchaseHeaderRepository
    {
        public PurchaseHeaderRepository(UnitOfWork context)
          : base(context)
        {

        }

        public int GetLatestSerialNumber(DateTime month)
        {
            var latestObj = FindBy(x => x.CreateDate.Year == month.Year && x.CreateDate.Month == month.Month)
                           .OrderByDescending(x => x.SerialNo).FirstOrDefault();
            return latestObj == null ? 0 : latestObj.SerialNo;
        }

        public PurchaseHeader GetPurchaseHeader(string purchaseNumber)
        {
            return FindBy(x => x.PurchaseNumber == purchaseNumber).Include(x=>x.Purchases.Select(z=>z.PurchaseApplication.PurchaseApplicationHeader)).FirstOrDefault();
        }

        public IList<PurchaseHeader> GetPurchaseHeaders(RequestCategoriesEnum category)
        {
            IList<PurchaseHeader> listOfPurchaseHeader;
            if (category == RequestCategoriesEnum.采购退货)
            {
                listOfPurchaseHeader = FindBy(x => x.RequestCategory == category).Include(x => x.Purchases.Select(s => s.PurchaseApplication.Item.Positions))
                        .ToList();
            }
            else
            {
                listOfPurchaseHeader = FindBy(x => x.RequestCategory != RequestCategoriesEnum.采购退货).Include(x => x.Purchases.Select(s => s.PurchaseApplication.Item.Positions))
                            .ToList();
            }

            return listOfPurchaseHeader;
        }
    }
    #endregion;

    #region PurchaseRepository
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(UnitOfWork context)
           : base(context)
        {

        }

        public void AddRange(IList<Purchase> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }
    }
    #endregion
}
