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
using App.Helper;
using NewServices.Interfaces;
using Core.Repository.Interfaces;
using System.Windows.Controls;
using Core.Model;

namespace App.Views.管理
{
    public partial class UnitManagementCtrl : CRUDCtrl //XtraUserControl // CRUDCtrl
    {
        private IManagementService _managementService;
        private RefreshHelper _refreshHelper;

        public UnitManagementCtrl(IManagementService managemntService) : base()
        {
            _managementService = managemntService;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.gridControl1.DataSource = _managementService.GetUnitModels();
            colId.Visible = false;

            //_refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.Id).ToString());
        }

        public void RefreshData(bool isSaveState = false)
        {
            this.gridControl1.DataSource = _managementService.GetUnitModels();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            UnitModel row = this.gridView1.GetFocusedRow() as UnitModel;
            _managementService.UpdateUnitModel(row);

            RefreshData(true);
           
        }

        private void gridView1_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            UnitModel row = this.gridView1.GetFocusedRow() as UnitModel;

            //_managementService.DeleteUnitModel(row);
        }

        private void gridView1_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            RefreshData(true);
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                //this.colUsername.OptionsColumn.AllowFocus = true;
                //this.colRole.OptionsColumn.AllowFocus = true;
                //this.colIsActive.OptionsColumn.AllowFocus = true;
            }
            if (e.Button.ButtonType == NavigatorButtonType.Edit)
            {
                //this.colUsername.OptionsColumn.AllowFocus = false;
                //this.colRole.OptionsColumn.AllowFocus = true;
                //this.colIsActive.OptionsColumn.AllowFocus = true;
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetRowCellValue(e.RowHandle, "ItemName", "aaaa");
        }
    }
}
