using System;
using System.Collections.Generic;
using Core.Enum;
using NewServices.Models.管理;

namespace NewServices.Interfaces
{
    public interface IManagementService
    {
        IList<PoNumberViewModel> GetAllPoNumbers();

        IList<ItemViewModel> GetAllItems();

        IList<SupplierViewModel> GetAllSuppliers();

        ItemViewModel GetItemById(Guid id);

        bool DeletePosition(PositionViewModel model);

        void CreateContract(ContractViewModel model);

        ContractViewModel GetContractByContractId(string contractId);

        int Upload(string filename, out string returnMsg, bool isInitial = false);

        int UploadRelationship(string filename, bool isInitial = false);

        Guid GetPoNumberIfNotExisting(string poNumber);

        IList<string> GetPositionNameByCode(string code);
        void UpdateItem(ItemViewModel model);
        void UpdatePo(PoNumberViewModel model);
        void UpdateContract(ContractViewModel model);

        void UpdateSupplier(SupplierViewModel model);
        int CreateSupplier(string name);

        void UpdateStorage(string code, string position, double total, string inStockNumber);
        bool TransferStorage(PositionViewModel model);
        void UpdateItemPrice(Guid itemId, double price);
        bool ExportItemExcel(string path, ReportNameEnum reportType, DateTime selectedMonth);

        int UpdateTotalNumberBySummarySheet(string filename, out string returnMsg);
    }
}
