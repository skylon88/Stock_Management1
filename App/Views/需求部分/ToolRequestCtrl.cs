using NewServices;
using App.Views.需求部分;
using Core.Enum;
using DevExpress.XtraGrid.Views.Grid;
using App.Helper;
using DevExpress.XtraSplashScreen;
using System;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using NewServices.Interfaces;
using NewServices.Models.需求部分;

namespace App.Views
{
    public partial class ToolRequestCtrl : RequestHeadersCtrl
    {
        public ToolRequestCtrl(IRequestService requestService, IManagementService managementService) : base(requestService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.工具维修);
        }

        protected override void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            base.gridView1_MasterRowExpanded(sender, e);

            GridView gridView = sender as GridView;
            GridView detailView = (GridView)gridView.GetDetailView(e.RowHandle, e.RelationIndex);

            gridView.Columns[nameof(parentModel.PoNumber)].Visible = false;
            gridView.Columns[nameof(parentModel.ContractId)].Visible = false;
            gridView.Columns[nameof(parentModel.ContractAddress)].Visible = false;

            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            detailView.Columns[nameof(childModel.ItemId)].Visible = false;
            detailView.Columns[nameof(childModel.ToApplyTotal )].Visible = false;
            detailView.Columns[nameof(childModel.ToDestroy)].Visible = false;


            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(subChildModel.PositionName)]);

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
                switch (processStatus)
                {
                    case ProcessStatusEnum.维修中:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    default:
                        RenderCommonHelper.SetColNotEditable(e.Column);
                        break;
                }
            }
            if (e.Column.FieldName == nameof(childModel.ToDestoryTotal))
            {
                switch (processStatus)
                {
                    case ProcessStatusEnum.维修完成:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    case ProcessStatusEnum.工程车维修入库:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    default:
                        RenderCommonHelper.SetColNotEditable(e.Column);
                        break;
                }
            }
        }


        protected override void detailView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            RequestViewModel model;
            GridView detailView = sender as GridView;
            detailView.ClearColumnErrors();
            GridColumn colFixAddress = detailView.Columns[nameof(model.FixAddress)];
            GridColumn colContact = detailView.Columns[nameof(model.Contact)];
            GridColumn colPhone = detailView.Columns[nameof(model.Phone)];
            GridColumn colFixingFinishDate = detailView.Columns[nameof(model.FixingFinishDate)];
            GridColumn colFixingDays = detailView.Columns[nameof(model.FixingDays)];
            GridColumn colFixingPrice = detailView.Columns[nameof(model.FixingPrice)];
            var fixAddress = detailView.GetRowCellValue(e.RowHandle, colFixAddress);
            var contact = detailView.GetRowCellValue(e.RowHandle, colContact);
            var phone = detailView.GetRowCellValue(e.RowHandle, colPhone);
            var fixingFinishDate = detailView.GetRowCellValue(e.RowHandle, colFixingFinishDate);
            var fixingDays = detailView.GetRowCellValue(e.RowHandle, colFixingDays);
            var fixingPrice = detailView.GetRowCellValue(e.RowHandle, colFixingPrice);
            if (fixAddress  == null)
            {
                e.Valid = false;
                detailView.SetColumnError(colFixAddress, "请输入维修地址");
            }
            if (contact == null)
            {
                e.Valid = false;
                detailView.SetColumnError(colContact, "请输入联系人");
            }
            if (phone == null)
            {
                e.Valid = false;
                detailView.SetColumnError(colPhone, "请输入联系人电话");
            }
        }

        public override bool IsValidated(IList<RequestViewModel> items)
        {
            StringBuilder sb = new StringBuilder();
            if (items.Any(x => String.IsNullOrWhiteSpace(x.FixAddress)))
            {
                sb.AppendLine("请输入维修地址!");
                
            }
            if (items.Any(x => String.IsNullOrWhiteSpace(x.Contact)))
            {
                sb.AppendLine("请输入联系人!");

            }
            if (items.Any(x => String.IsNullOrWhiteSpace(x.Phone)))
            {
                sb.AppendLine("请输入联系人电话!");

            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return false;
            }
            return true;
        }
    }
}
