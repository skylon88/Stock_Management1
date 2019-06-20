using System.Drawing;
using App.Views.需求部分;
using NewServices;
using Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using NewServices.Interfaces;
using System;

namespace App.Views
{
    public partial class RefundRequestCtrl : RequestHeadersCtrl
    {
        public RefundRequestCtrl(IRequestService requestService, IManagementService managementService) : base(requestService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.采购退货);
        }

        protected override void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            base.gridView1_MasterRowExpanded(sender, e);

            GridView gridView = sender as GridView;
            GridView detailView = (GridView)gridView.GetDetailView(e.RowHandle, e.RelationIndex);

            detailView.Columns[nameof(childModel.ToApplyTotal)].Caption = "可申请退货";

            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            detailView.Columns[nameof(childModel.ItemId)].Visible = false;
            detailView.Columns[nameof(childModel.ToInStockTotal)].Visible = false;
            detailView.Columns[nameof(childModel.ToDestoryTotal)].Visible = false;
            detailView.Columns[nameof(childModel.DestoriedTotal)].Visible = false;
            detailView.Columns[nameof(childModel.FixAddress)].Visible = false;
            detailView.Columns[nameof(childModel.Phone)].Visible = false;
            detailView.Columns[nameof(childModel.Contact)].Visible = false;
            detailView.Columns[nameof(childModel.FixingPrice)].Visible = false;
            detailView.Columns[nameof(childModel.FixingDays)].Visible = false;
            detailView.Columns[nameof(childModel.FixingFinishDate)].Visible = false;
            detailView.Columns[nameof(childModel.ToDestroy)].Visible = false;

        }

        protected override void detailView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            base.detailView1_RowCellStyle(sender, e);

            GridView currentView = sender as GridView;

            if (e.Column.FieldName == nameof(childModel.ToApplyTotal))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Status)).ToString(), out ProcessStatusEnum processStatus);

                var total = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Total));
                var toApplyTotal = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.ToApplyTotal));
                if (total != toApplyTotal && processStatus == ProcessStatusEnum.需求建立)
                {
                    e.Appearance.BackColor = Color.LightPink;
                }

            }

            if (e.Column.FieldName == nameof(childModel.Total))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Status)).ToString(), out ProcessStatusEnum processStatus);

                var total = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Total));
                var toApplyTotal = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.ToApplyTotal));
                if (total != toApplyTotal && processStatus == ProcessStatusEnum.需求建立)
                {
                    e.Appearance.BackColor = Color.LightPink;
                }

            }
        }
    }
}
