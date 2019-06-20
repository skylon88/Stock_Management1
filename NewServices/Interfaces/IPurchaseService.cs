using System;
using System.Collections.Generic;
using Core.Enum;
using NewServices.Models.采购部分;
using NewServices.Models.需求部分;

namespace NewServices.Interfaces
{
    public interface IPurchaseService
    {
        #region 采购申请

        void CreateApplication(IList<RequestViewModel> model, string poNumber, string temp_ApplicationNumber);
        bool UpdateApplication(PurchaseApplicationViewModel model);
        bool DeleteApplication(PurchaseApplicationViewModel model);
        void CopyApplication(PurchaseApplicationViewModel model);

        void ConfirmAllApplications(IList<PurchaseApplicationViewModel> models);

        IList<PurchaseApplicationHeaderViewModel> GetAllPurchaseApplicationHeaderViewModels(RequestCategoriesEnum category);

        bool ExportPurchaseApplicatoin(IList<PurchaseApplicationViewModel> models, string path);

        IList<PurchaseViewModel> GetAllPurchaseNumberByItemCode(string code = null);

        #endregion

        #region 采购

        int CreatePurchase(IList<PurchaseApplicationViewModel> model, string temp_purchaseNumber);

        bool UpdatePurchaseHeader(PurchaseHeaderViewModel model);

        void UpdatePurchase(PurchaseViewModel model);
        void DeletePurchase(PurchaseViewModel model);

        IList<PurchaseHeaderViewModel> GetAllPurchaseViewModels(RequestCategoriesEnum category);

        void FillAllPurchaseTotal(IList<PurchaseViewModel> models);
        void UpdatePurchaseProcessStatus(Guid purchaseId, ProcessStatusEnum status);

        bool ExportPurchase(IList<PurchaseViewModel> models, string path, bool isMerge =false);
        #endregion

    }
}
