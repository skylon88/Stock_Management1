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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using App.Helper;
using NewServices.Interfaces;
using NewServices.Models.采购部分;

namespace App.Views.采购部分
{
    public partial class RefundApplicationCtrl : PurchaseApplicationCtrl
    {
        private PurchaseViewModel PurchaseModel;
        private IList<PurchaseViewModel> listOfPurchaseViewModels;

        public RefundApplicationCtrl(IPurchaseService purchaseService, IManagementService managementService):base(purchaseService, managementService)
        {
            SetCurrentRequestCategory(RequestCategoriesEnum.采购退货);
            colApplicationNumber.Caption = @"退货申请号";
            //colSelectedPurchaseNumber.Visible = true;
            //RenderCommonHelper.SetColNotEditable(colSupplierName);
            listOfPurchaseViewModels = _purchaseService.GetAllPurchaseNumberByItemCode();
            RefreshData();

            //this.gridControl1.DataSource = _purchaseService.GetAllPurchaseApplicationViewModels(_currentRequestCategory);
        }

        protected override void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            base.gridView1_MasterRowExpanded(sender, e);

            //GridView gridView = sender as GridView;
            //GridView detailView = (GridView)gridView.GetDetailView(e.RowHandle, e.RelationIndex);

        }


        protected override void detailView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView currentView = sender as GridView;

            if (e.Column.FieldName == nameof(childModel.SelectedPurchaseNumber))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);

                if (auditStatus == AuditStatusEnum.未审批)
                {
                    PurchaseApplicationViewModel model = (PurchaseApplicationViewModel)currentView.GetRow(e.RowHandle);
                    RepositoryItemLookUpEdit purchaseRepItem = new RepositoryItemLookUpEdit();
                    purchaseRepItem.Name = "DropDownForPurchaseNumber";
                    purchaseRepItem.NullText = "";
                    purchaseRepItem.ValueMember = nameof(PurchaseModel.PurchaseNumber);
                    purchaseRepItem.DisplayMember = nameof(PurchaseModel.PurchaseNumber);
                    purchaseRepItem.DataSource = listOfPurchaseViewModels.Where(x => x.Code == model.Code);
                    purchaseRepItem.PopulateColumns();
                    purchaseRepItem.PopupWidth = 500;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.PurchaseId)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.RequestId)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.ApplicationNumber)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.PoNumber)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Category)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Name)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Code)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Brand)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Model)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Specification)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Dimension)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Unit)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.DefaultPrice)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Price)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.TotalPrice)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.ReadyForInStock)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Status)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.IsDeleted)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.AlreadyInStock)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Position)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.Note)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.DeliveryDate)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.UpdateDate)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.IsPriceChange)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.ItemId)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.LastDeliveryDate)].Visible = false;
                    //purchaseRepItem.Columns[nameof(PurchaseModel.CorrectionTotal)].Visible = false;

                    //gridControl1.RepositoryItems.Add(positionRepItem);
                    e.RepositoryItem = purchaseRepItem;
                    purchaseRepItem.EditValueChanged -= RepositoryItemLookUpEdit1_EditValueChanged;
                    purchaseRepItem.EditValueChanged += RepositoryItemLookUpEdit1_EditValueChanged;

                    RenderCommonHelper.SetColEditable(e.Column);
                }
                else if (auditStatus == AuditStatusEnum.已审批)
                {
                    RenderCommonHelper.SetColNotEditable(e.Column);
                }
            }
            if (e.Column.FieldName == nameof(parentModel.TotalConfirmed))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
                if (auditStatus == AuditStatusEnum.已审批)
                {
                    RenderCommonHelper.SetColNotEditable(e.Column);
                }
                if (auditStatus == AuditStatusEnum.未审批)
                {
                    RenderCommonHelper.SetColEditable(e.Column);
                }

            }

            if (e.Column.FieldName == nameof(parentModel.AuditStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.ProcessStatus)).ToString(), out ProcessStatusEnum processStatus);
                if (processStatus == ProcessStatusEnum.申请审核中)
                {
                    RenderCommonHelper.SetColEditable(e.Column);
                }
                if (processStatus != ProcessStatusEnum.申请审核中)
                {
                    RenderCommonHelper.SetColNotEditable(e.Column);
                }
            }
        }

        private void RepositoryItemLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            GridView gridView = sender as GridView;
            object editValue = edit.EditValue;
            int index = edit.Properties.GetDataSourceRowIndex(edit.Properties.ValueMember, editValue);
            PurchaseViewModel row = (PurchaseViewModel)edit.Properties.GetDataSourceRowByKeyValue(editValue);
            PurchaseApplicationViewModel rowPurchaseApplication = (PurchaseApplicationViewModel)gridView.GetFocusedRow();
            rowPurchaseApplication.SelectedPurchaseNumber = row.PurchaseNumber;
            rowPurchaseApplication.PoNumber = row.PoNumber;
            rowPurchaseApplication.SupplierName = row.SupplierCode;
            rowPurchaseApplication.CurrentPurchasePrice = row.Price;
            //PurchaseApplicationViewModel rowView = (PurchaseApplicationViewModel)edit.GetSelectedDataRow();
            //DataRow row1 = rowView.Row;
            //_requestService.UpdateRequest(row);
        }
    }
}
