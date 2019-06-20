using System;
using System.Windows.Forms;
using NewServices;
using DevExpress.XtraGrid.Views.Grid;
using App.Helper;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using System.ComponentModel;
using NewServices.Interfaces;
using NewServices.Models.管理;

namespace App.Views.管理
{
    public partial class SupplierManagementCtrl : CRUDCtrl
    {
        private IManagementService _managementService;
        private RefreshHelper _refreshHelper;
        private SupplierViewModel parentModel;
        public SupplierManagementCtrl(IManagementService managemntService) : base() {
            _managementService = managemntService;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.gridControl1.DataSource = _managementService.GetAllSuppliers();
            colId.Visible = false;

            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.Id).ToString());
        }


        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            GridView gridView1 = sender as GridView;
            SupplierViewModel row = this.gridView1.GetFocusedRow() as SupplierViewModel;

            _managementService.UpdateSupplier(row);
            RefreshData(true);
        }

        public void CreateNewSuppiler(string name)
        {
            int result = _managementService.CreateSupplier(name);

            if(result == 0)
            {
                MessageBox.Show("供应商名字已存在");
            }
        }

        public void RefreshData(bool isSaveState = false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _managementService.GetAllSuppliers();
            if (isSaveState) _refreshHelper.LoadViewInfo();
        
        }

    }
}
