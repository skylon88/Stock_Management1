using System;
using App.Helper;
using App.Views.需求部分;
using Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using NewServices;
using NewServices.Interfaces;

namespace App.Views
{
    public partial class ReturnRequestCtrl : RequestHeadersCtrl
    {
        public ReturnRequestCtrl(IRequestService requestService, IManagementService managementService) : base(requestService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.物品退回);
        }

        protected override void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            base.gridView1_MasterRowExpanded(sender, e);

            GridView gridView = sender as GridView;
            GridView detailView = (GridView)gridView.GetDetailView(e.RowHandle, e.RelationIndex);

            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            detailView.Columns[nameof(childModel.ItemId)].Visible = false;
            detailView.Columns[nameof(childModel.ToApplyTotal)].Visible = false;
            detailView.Columns[nameof(childModel.ToOutStockTotal)].Visible = false;
            detailView.Columns[nameof(childModel.OutStockTotal)].Visible = false;
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

        protected override void detailView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            base.detailView_CustomRowCellEdit(sender, e);

            GridView detailView = sender as GridView;
            Enum.TryParse(detailView.GetRowCellValue(e.RowHandle, nameof(parentModel.Status)).ToString(), out ProcessStatusEnum processStatus);
            Enum.TryParse(detailView.GetRowCellValue(e.RowHandle, nameof(parentModel.RequestCategory)).ToString(), out RequestCategoriesEnum requestCategory);

            if (e.Column.FieldName == nameof(childModel.ToOutStockTotal))
            {
                RenderCommonHelper.SetColNotEditable(e.Column);
            }
            if (e.Column.FieldName == nameof(childModel.ToInStockTotal))
            {
                RenderCommonHelper.SetColNotEditable(e.Column);
            }
        }

    }
}
