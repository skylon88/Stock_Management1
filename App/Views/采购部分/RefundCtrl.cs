using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewServices;
using Core.Enum;
using NewServices.Interfaces;

namespace App.Views.采购部分
{
    public partial class RefundCtrl : PurchaseCtrl
    {
        public RefundCtrl(IPurchaseService purchaseService, IManagementService managementService) : base(purchaseService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.采购退货);
            RefreshData();
        }
    }
}
