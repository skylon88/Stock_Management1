using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewServices.Models.入库部分;
using NewServices.Models.出库部分;
using NewServices.Models.采购部分;
using NewServices.Models.需求部分;

namespace NewServices
{
    public interface IStockService
    {
        #region 入库

        bool UpdateInStockHeader(InStockHeaderViewModel model);
        void CreateInStock(IList<PurchaseViewModel> models, string temp_inStockNumber);
        void CreateInStock(IList<RequestViewModel> models, string temp_inStockNumber);

        IList<InStockHeaderViewModel> GetAllInStockHeaderViewModel();
  
        bool ExportInStock(IList<InStockHeaderViewModel> models, string path);

        #endregion

        #region 出库

        bool UpdateOutStockHeader(OutStockHeaderViewModel model);

        void CreateOutStock(IList<RequestViewModel> models, string temp_outStockNumber);

        IList<OutStockHeaderViewModel> GetAllOutStockHeaderViewModel();

        bool ExportOutStock(IList<OutStockHeaderViewModel> models, string path);

        #endregion
    }
}
