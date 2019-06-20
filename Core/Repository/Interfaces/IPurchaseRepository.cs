using System;
using System.Collections.Generic;
using Core.Data;
using Core.Enum;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IPurchaseHeaderRepository : IGenericRepository<PurchaseHeader>
    {
        int GetLatestSerialNumber(DateTime month);
        PurchaseHeader GetPurchaseHeader(string purchaseNumber);
        IList<PurchaseHeader> GetPurchaseHeaders(RequestCategoriesEnum category);
    }

    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        void AddRange(IList<Purchase> entities);

    }

}
