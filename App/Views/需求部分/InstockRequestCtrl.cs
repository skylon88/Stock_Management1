using App.Helper;
using App.Views.需求部分;
using Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using NewServices;
using NewServices.Interfaces;


namespace App.Views.需求部分
{
    public partial class InstockRequestCtrl : RequestHeadersCtrl
    {
        public InstockRequestCtrl(IRequestService requestService, IManagementService managementService) : base(requestService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.购入需求);
        }

        protected override void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            base.gridControl1_ViewRegistered(sender, e);

            GridView detailView = e.View as GridView;
            if (detailView.Columns[0].Name != "colPositionName")
            {

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

                RenderCommonHelper.SetColEditable(detailView.Columns[nameof(childModel.PositionName)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ToInStockTotal)]);
            }
        }
    }
}
