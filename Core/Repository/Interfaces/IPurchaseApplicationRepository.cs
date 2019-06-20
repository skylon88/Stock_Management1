using System;
using System.Collections.Generic;
using Core.Data;
using Core.Model;

namespace Core.Repository.Interfaces
{
    public interface IPurchaseApplicationHeaderRepository : IGenericRepository<PurchaseApplicationHeader>
    {
        int GetLatestSerialNumber(DateTime month);

        IList<PurchaseApplicationHeader> GetAllPurchaseApplicationHeaders();
    }

    public interface IPurchaseApplicationRepository : IGenericRepository<PurchaseApplication>
    {
        void AddRange(IList<PurchaseApplication> entities);

        IList<PurchaseApplication> GetAllPurchaseApplications();
    }
}
