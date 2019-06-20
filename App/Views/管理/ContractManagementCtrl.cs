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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Base;
using App.Helper;
using NewServices.Interfaces;
using NewServices.Models.管理;
using NewServices.Models.需求部分;

namespace App.Views.管理
{
    public partial class ContractManagementCtrl : CRUDCtrl
    {
        private IManagementService _managemntService;
        private RefreshHelper _refreshHelper;
        private PoNumberViewModel parentModel;
        public ContractManagementCtrl(IManagementService managemntService)
        {
            _managemntService = managemntService;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.gridControl1.DataSource = _managemntService.GetAllPoNumbers();

            colPOId.Visible = false;
            RenderCommonHelper.SetColNotEditable(colCreateDate);
            RenderCommonHelper.SetColNotEditable(colUpdateDate);

            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.PoNumber).ToString());
        }

        protected void gridView1_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
        }
        private void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            ContractViewModel model;
            GridView gridView = sender as GridView;
            GridView detailView = (GridView)gridView.GetDetailView(e.RowHandle, e.RelationIndex);

            detailView.MasterRowExpanding -= detailView_MasterRowExpanding;
            detailView.MasterRowExpanding += detailView_MasterRowExpanding;

            detailView.MasterRowExpanded -= detailView_MasterRowExpanded;
            detailView.MasterRowExpanded += detailView_MasterRowExpanded;

            detailView.RowUpdated -= detailView1_RowUpdated;
            detailView.RowUpdated += detailView1_RowUpdated;

            detailView.Columns[nameof(model.Id)].Visible = false;

            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.Id)]);
            RenderCommonHelper.SetColEditable(detailView.Columns[nameof(model.ContractNumber)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.CreateDate)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.UpdateDate)]);

            SplashScreenManager.CloseDefaultWaitForm();
        }

        protected void detailView_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
        }

        private void detailView_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            GridView detailView = (GridView)gridView.GetDetailView(e.RowHandle, e.RelationIndex);
            if (detailView != null)
            {
                RequestHeaderViewModel model = (RequestHeaderViewModel)detailView.GetRow(e.RowHandle);

                detailView.OptionsBehavior.Editable = false;
                detailView.OptionsBehavior.ReadOnly = true;

                
            }

            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            PoNumberViewModel row = (PoNumberViewModel)this.gridView1.GetFocusedRow();
            _managemntService.UpdatePo(row);
            SplashScreenManager.CloseDefaultWaitForm();
            RefreshData();
        }

        private void detailView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            var detailView = sender as GridView;
            ContractViewModel row = (ContractViewModel)detailView.GetFocusedRow();
            _managemntService.UpdateContract(row);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        public void RefreshData(bool isSaveState = false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _managemntService.GetAllPoNumbers();
            if (isSaveState) _refreshHelper.LoadViewInfo();
        }

        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }


    }
}
