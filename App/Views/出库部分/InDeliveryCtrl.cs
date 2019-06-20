
using NewServices;
using App.Helper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using System;
using Core.Enum;
using NewServices.Models.出库部分;

namespace App.Views.出库部分
{
    public partial class InDeliveryCtrl : CRUDCtrl
    {
        private IStockService _stockService;
        private RefreshHelper _refreshHelper;
        private OutStockHeaderViewModel parentModel;
        private OutStockViewModel childModel;

        public InDeliveryCtrl(IStockService stockService)
        {
            _stockService = stockService;
            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.OutStockNumber).ToString());

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            //this.gridControl1.DataSource = _stockService.GetAllOutInstock();

            RenderCommonHelper.SetColNotEditable(colOutStockCategory);
            RenderCommonHelper.SetColNotEditable(colRequestNumber);
            RenderCommonHelper.SetColNotEditable(colAddress);
            RenderCommonHelper.SetColNotEditable(colContractNumber);
        }

        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            OutStockHeaderViewModel row = (OutStockHeaderViewModel)this.gridView1.GetFocusedRow();
            _stockService.UpdateOutStockHeader(row);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        public void RefreshData(bool isSaveState = false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _stockService.GetAllOutStockHeaderViewModel();
            if (isSaveState) _refreshHelper.LoadViewInfo();
        }

        private void gridView1_MasterRowGetRelationDisplayCaption(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "出库详细";
        }

        //Register detailView
        private void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            GridView detailView = e.View as GridView;
            detailView.OptionsSelection.MultiSelect = false;

            detailView.RowCellStyle -= detailView1_RowCellStyle;
            detailView.RowCellStyle += detailView1_RowCellStyle;

            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Name)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Code)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Brand)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Model)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Specification)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Dimension)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Unit)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Price)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Total)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.TotalPrice)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.PositionName)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ProcessStatus)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.UpdateDate)]);

            detailView.Columns[nameof(childModel.Price)].DisplayFormat.FormatType = FormatType.Numeric;
            detailView.Columns[nameof(childModel.Price)].DisplayFormat.FormatString = "c2";
            detailView.Columns[nameof(childModel.TotalPrice)].DisplayFormat.FormatType = FormatType.Numeric;
            detailView.Columns[nameof(childModel.TotalPrice)].DisplayFormat.FormatString = "c2";
        }

        private void gridView1_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
        }

        private void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            SplashScreenManager.CloseDefaultWaitForm();
        }

        public IList<OutStockHeaderViewModel> GetSelectedHeaderRows()
        {
            IList<OutStockHeaderViewModel> instockHeaderViewModels = new List<OutStockHeaderViewModel>();
            var parentRows = gridView1.GetSelectedRows();
            foreach (var item in parentRows)
            {
                instockHeaderViewModels.Add((OutStockHeaderViewModel)gridView1.GetRow(item));
            }
            return instockHeaderViewModels;
        }

        protected void detailView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            e.Appearance.BackColor = Color.Beige;

            if (e.Column.FieldName == nameof(childModel.ProcessStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.ProcessStatus)).ToString(), out ProcessStatusEnum processStatus);
                RenderCommonHelper.SetStatusColor(processStatus, e);
            }
        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == nameof(parentModel.OutStockCategory))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.OutStockCategory)).ToString(), out RequestCategoriesEnum requestCategories);
                RenderCommonHelper.SetCategoryColor(requestCategories, e);

            }

        }
    }
}
