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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using Core.Enum;
using DevExpress.XtraEditors.Controls;
using App.Helper;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using NewServices.Interfaces;
using NewServices.Models.采购部分;
using DevExpress.XtraSplashScreen;

namespace App.Views.采购部分
{
    public partial class PurchaseApplicationCtrl : CRUDCtrl
    {
        protected IPurchaseService _purchaseService;
        protected IManagementService _managementService;
        protected RequestCategoriesEnum _currentRequestCategory;
        protected RefreshHelper _refreshHelper;
        protected PurchaseApplicationHeaderViewModel parentModel;
        protected PurchaseApplicationViewModel childModel;
        private RepositoryItemLookUpEdit supplierRepItem;

        public PurchaseApplicationCtrl(IPurchaseService purchaseService, IManagementService managementService) 
        {
            _purchaseService = purchaseService;
            _managementService = managementService;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            SetCurrentRequestCategory(RequestCategoriesEnum.材料需求);

            RenderCommonHelper.SetColNotEditable(colApplicationNumber);
            RenderCommonHelper.SetColNotEditable(colRequestHeaderNumber);
            RenderCommonHelper.SetColNotEditable(colPoNumber);
            RenderCommonHelper.SetColNotEditable(colTotalApplied);
            RenderCommonHelper.SetColNotEditable(colTotalConfirmed);
            RenderCommonHelper.SetColNotEditable(colAuditStatus);
            RenderCommonHelper.SetColNotEditable(colStatus);
            RenderCommonHelper.SetColNotEditable(colCompletePercentage);
            RenderCommonHelper.SetColNotEditable(colCreateDate);
            RenderCommonHelper.SetColNotEditable(colUpdateDate);

            colCompletePercentage.DisplayFormat.FormatType = FormatType.Numeric; colCompletePercentage.DisplayFormat.FormatString = "P";
            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.ApplicationNumber));
        }

        private void detailView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var detailView = sender as GridView;
            PurchaseApplicationViewModel row = detailView.GetFocusedRow() as PurchaseApplicationViewModel;
            _purchaseService.UpdateApplication(row);
        }

        #region //拷贝
        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("确定拷贝", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;

            }
            else
            {
                var view = gridControl1.FocusedView as GridView;
                PurchaseApplicationViewModel row = (PurchaseApplicationViewModel)view.GetFocusedRow();
                //int rowindex = this.gridView1.FocusedRowHandle;
                //var row = (PurchaseApplicationViewModel)this.gridView1.GetRow(rowindex);
                _purchaseService.CopyApplication(row);
                RefreshData(true);
            }
        }
        private void detailView1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (hi.InRowCell)
                {
                    if (hi.Column.RealColumnEdit.GetType() == typeof(RepositoryItemButtonEdit))
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        view.FocusedColumn = hi.Column;
                        view.ShowEditor();
                        //force button click 
                        ButtonEdit edit = (view.ActiveEditor as ButtonEdit);
                        Point p = view.GridControl.PointToScreen(e.Location);
                        p = edit.PointToClient(p);
                        EditHitInfo ehi = (edit.GetViewInfo() as ButtonEditViewInfo).CalcHitInfo(p);
                        if (ehi.HitTest == EditHitTest.Button)
                        {
                            edit.PerformClick(ehi.HitObject as EditorButton);
                            ((DevExpress.Utils.DXMouseEventArgs)e).Handled = true;
                        }
                    }
                }
            }
        }
        #endregion

        //动态读取每个Item对应的供应商
        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            /*
            if (gridView1.FocusedColumn.FieldName != "SupplierName") return;
            LookUpEdit editor = (LookUpEdit)gridView1.ActiveEditor;
            PurchaseApplicationViewModel item = (PurchaseApplicationViewModel)gridView1.GetFocusedRow();
            var list = new List<string>();
            if (!string.IsNullOrEmpty(item.FirstSupplier)) list.Add(item.FirstSupplier);
            if (!string.IsNullOrEmpty(item.SecondSupplier)) list.Add(item.SecondSupplier);
            if (!string.IsNullOrEmpty(item.ThirdSupplier)) list.Add(item.ThirdSupplier);
            editor.Properties.DataSource = list;
            */
        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == nameof(parentModel.AuditStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
                if (auditStatus == AuditStatusEnum.已审批)
                {
                    e.Appearance.BackColor = Color.GreenYellow;
                }
                if (auditStatus == AuditStatusEnum.未审批)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }

            if(e.Column.FieldName == nameof(parentModel.Status))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.Status)).ToString(), out ProcessStatusEnum processStatus);
                RenderCommonHelper.SetStatusColor(processStatus, e);
            }
            if (e.Column.FieldName == nameof(parentModel.CompletePercentage))
            {
                decimal CompletePercentage = (decimal)currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.CompletePercentage));
                if (CompletePercentage == 1)
                {
                    e.Appearance.BackColor = Color.LawnGreen;
                }
                else e.Appearance.BackColor = Color.DarkOrange;
            }

        }

        //GridView Color
        private void detailView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            PurchaseApplicationViewModel model;
            GridView currentView = sender as GridView;

            if (e.Column.FieldName == nameof(model.AuditStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(model.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(model.ProcessStatus)).ToString(), out ProcessStatusEnum processStatus);
                if (auditStatus == AuditStatusEnum.已审批)
                {
                    e.Appearance.BackColor = Color.GreenYellow;
                }
                if (auditStatus == AuditStatusEnum.未审批)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
            }

            if (e.Column.FieldName == nameof(model.TotalConfirmed))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(model.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
                double totalConfirm = (double)currentView.GetRowCellValue(e.RowHandle, nameof(model.TotalConfirmed));
                if (totalConfirm <= 0)
                {
                    e.Appearance.BackColor = Color.Red;
                }

            }

            if (e.Column.FieldName == nameof(model.ProcessStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(model.ProcessStatus)).ToString(), out ProcessStatusEnum processStatus);
                RenderCommonHelper.SetStatusColor(processStatus, e);
            }
        }

        
        #region SetupButtonStatus
        private void detailView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            SetupButtonStatus();
        }

        //审核Button
        private void SetupAuditingButtonStatus(Main main, IList<PurchaseApplicationViewModel> listOfPurchaseApplications)
        {
            if(listOfPurchaseApplications.Count == 0)
            {
                main.barButtonItem15.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x => string.IsNullOrEmpty(x.SupplierName)))
            {
                main.barButtonItem15.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.All(x => x.AuditStatus == AuditStatusEnum.未审批))
            {
                main.barButtonItem15.Enabled = true;
                return;
            }
        }

        //生成采购单Button
        private void SetupCreatePurchaseButton(Main main, IList<PurchaseApplicationViewModel> listOfPurchaseApplications)
        {
            if (listOfPurchaseApplications.Count == 0)
            {
                main.barButtonItem18.Enabled = false;
                return;
            }
            if (_currentRequestCategory != RequestCategoriesEnum.材料需求)
            {
                main.barButtonItem18.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x=>x.AuditStatus == AuditStatusEnum.未审批)){
                main.barButtonItem18.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x => x.ProcessStatus != ProcessStatusEnum.申请审核中))
            {
                main.barButtonItem18.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x => x.TotalConfirmed < 0))
            {
                main.barButtonItem18.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(o => o.PoNumber != listOfPurchaseApplications[0].PoNumber))//所选择的采购申请PO号必须保持一致
            {
                main.barButtonItem18.Enabled = false;
                return;
            }
            //if (listOfPurchaseApplications.Any(o => o.SupplierName != listOfPurchaseApplications[0].SupplierName))//所选择的供应商必须保持一致
            //{
            //    main.barButtonItem18.Enabled = false;
            //    return;
            //}
            main.barButtonItem18.Enabled = true;
            return;
        }

        //生成退货单Button
        private void SetupRefundPurchaseButton(Main main, IList<PurchaseApplicationViewModel> listOfPurchaseApplications)
        {
            if (listOfPurchaseApplications.Count == 0)
            {
                main.barButtonItem46.Enabled = false;
                return;
            }
            if (_currentRequestCategory != RequestCategoriesEnum.采购退货)
            {
                main.barButtonItem46.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x => x.AuditStatus == AuditStatusEnum.未审批))
            {
                main.barButtonItem46.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x => x.ProcessStatus != ProcessStatusEnum.申请审核中))
            {
                main.barButtonItem46.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(x => x.TotalConfirmed <= 0))
            {
                main.barButtonItem46.Enabled = false;
                return;
            }
            if (listOfPurchaseApplications.Any(o => o.PoNumber != listOfPurchaseApplications[0].PoNumber))//所选择的采购申请PO号必须保持一致
            {
                main.barButtonItem46.Enabled = false;
                return;
            }
            //if (listOfPurchaseApplications.Any(o => o.SupplierName != listOfPurchaseApplications[0].SupplierName))//所选择的供应商必须保持一致
            //{
            //    main.barButtonItem46.Enabled = false;
            //    return;
            //}
            main.barButtonItem46.Enabled = true;
            return;
        }


        private void SetupButtonStatus()
        {
            var main = (Main)this.ParentForm;
            IList<PurchaseApplicationViewModel> PurchaseApplications = GetSelectedRows();
            if (main != null)
            {
                SetupAuditingButtonStatus(main, PurchaseApplications);
                SetupCreatePurchaseButton(main, PurchaseApplications);
                SetupRefundPurchaseButton(main, PurchaseApplications);
            }
        }
        #endregion

        #region Validation
        private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            PurchaseApplicationViewModel model;
            GridView view = sender as GridView;
            view.ClearColumnErrors();
            GridColumn coltotalConfirmed = view.Columns[nameof(model.TotalConfirmed)];
            double totalConfirmed = (double)view.GetRowCellValue(e.RowHandle, coltotalConfirmed);
            if (totalConfirmed < 0)
            {
                e.Valid = false;
                //Set errors with specific descriptions for the columns
                view.SetColumnError(coltotalConfirmed, "审核数量必须大于0");
                
            }
        }

        private void detailView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        #endregion

        #region Call from another controller
        public void RefreshData(bool isSaveState = false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _purchaseService.GetAllPurchaseApplicationHeaderViewModels(_currentRequestCategory);
            if (isSaveState) _refreshHelper.LoadViewInfo();
            SetupButtonStatus();
        }

        public IList<PurchaseApplicationViewModel> GetSelectedRows()
        {
            IList<PurchaseApplicationViewModel> purchaseApplicationViewModels = new List<PurchaseApplicationViewModel>();
            IList<GridView> detailViews = new List<GridView>();
            var parentRows = gridView1.RowCount;
            for (int p = 0; p < parentRows; p++)
            {
                var subView = gridView1.GetDetailView(p, 0) as GridView;
                if (subView != null)
                {
                    detailViews.Add(subView);
                }}
            foreach (var detail in detailViews)
            {
                var selectedRows = detail.GetSelectedRows();
                foreach (var s in selectedRows)
                {
                    purchaseApplicationViewModels.Add((PurchaseApplicationViewModel)detail.GetRow(s));
                }
            }
            return purchaseApplicationViewModels;
        }

        public void SetCurrentRequestCategory(RequestCategoriesEnum requestCategory)
        {
            _currentRequestCategory = requestCategory;
        }

        #endregion


        private void detailView1_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {

        }

        private void detailView1_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            var MessageBoxResult = MessageBox.Show("是否删除该选项", "是否删除该选项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MessageBoxResult == DialogResult.Yes)
            {
                GridView detailView = sender as GridView;
                PurchaseApplicationViewModel row = (PurchaseApplicationViewModel)detailView.GetFocusedRow();
                if (row.ProcessStatus > ProcessStatusEnum.申请审核中)
                {
                    MessageBox.Show("该状态下无法删除");
                    e.Cancel = true;
                    return;
                }
                var result = _purchaseService.DeleteApplication(row);
                if (!result)
                {
                    MessageBox.Show("此申请下已经生成采购单, 无法删除");
                    e.Cancel = true;
                    return;
                }
            }
            else if (MessageBoxResult != DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        protected virtual void detailView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView currentView = sender as GridView;

            if (e.Column.FieldName == nameof(childModel.SupplierName))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
                if (auditStatus == AuditStatusEnum.已审批)
                {
                    RenderCommonHelper.SetColNotEditable(e.Column);
                }
                if (auditStatus == AuditStatusEnum.未审批)
                {
                    RenderCommonHelper.SetColEditable(e.Column);
                }
            }

            if (e.Column.FieldName == nameof(childModel.TotalConfirmed))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
                if (auditStatus == AuditStatusEnum.已审批)
                {
                    RenderCommonHelper.SetColNotEditable(e.Column);
                }
                if (auditStatus == AuditStatusEnum.未审批)
                {
                    RenderCommonHelper.SetColEditable(e.Column);
                }

            }

            if (e.Column.FieldName == nameof(childModel.AuditStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.AuditStatus)).ToString(), out AuditStatusEnum auditStatus);
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

        private void gridView1_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
        }

        protected virtual void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void gridView1_MasterRowGetRelationDisplayCaption(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "采购申请详细";
        }

        private void gridControl1_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            GridView detailView = e.View as GridView;

            detailView.OptionsSelection.MultiSelect = true;
            detailView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            detailView.RowUpdated -= detailView1_RowUpdated;
            detailView.RowUpdated += detailView1_RowUpdated;

            detailView.RowCellStyle -= detailView1_RowCellStyle;
            detailView.RowCellStyle += detailView1_RowCellStyle;

            detailView.RowDeleted -= detailView1_RowDeleted;
            detailView.RowDeleted += detailView1_RowDeleted;

            detailView.RowDeleting -= detailView1_RowDeleting;
            detailView.RowDeleting += detailView1_RowDeleting;

            detailView.CustomRowCellEdit -= detailView1_CustomRowCellEdit;
            detailView.CustomRowCellEdit += detailView1_CustomRowCellEdit;

            detailView.ValidateRow -= gridView1_ValidateRow;
            detailView.ValidateRow += gridView1_ValidateRow;

            detailView.InvalidRowException -= detailView1_InvalidRowException;
            detailView.InvalidRowException += detailView1_InvalidRowException;

            detailView.MouseDown -= detailView1_MouseDown;
            detailView.MouseDown += detailView1_MouseDown;

            detailView.SelectionChanged -= detailView1_SelectionChanged;
            detailView.SelectionChanged += detailView1_SelectionChanged;

            var colCopy = new GridColumn()
            {
                Name = "Action",
                Visible = true,
                VisibleIndex = 26,
                ColumnEdit = this.repositoryItemButtonEdit1

            };


            //初始化供应商dropdownlist
            supplierRepItem = new RepositoryItemLookUpEdit();
            supplierRepItem.Name = "DropDownForSupplier";
            supplierRepItem.NullText = string.Empty;

            this.supplierRepItem.DataSource = _managementService.GetAllSuppliers().Select(x => x.Name);
            gridControl1.RepositoryItems.Add(supplierRepItem); ;
            GridColumn aColumn = detailView.Columns[nameof(childModel.SupplierName)];
            aColumn.ColumnEdit = supplierRepItem;

            detailView.Columns.Add(colCopy);
            detailView.Columns[nameof(childModel.SelectedPurchaseNumber)].Visible = false;
            detailView.Columns[nameof(childModel.PurchaseApplicationId)].Visible = false;
            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            detailView.Columns[nameof(childModel.RequestNumber)].Visible = false;
            detailView.Columns[nameof(childModel.ApplicationNumber)].Visible = false;
            detailView.Columns[nameof(childModel.RequestCategory)].Visible = false;
            detailView.Columns[nameof(childModel.ContractNo)].Visible = false;
            detailView.Columns[nameof(childModel.PoNumber)].Visible = false;
            detailView.Columns[nameof(childModel.Category)].Visible = false;
            detailView.Columns[nameof(childModel.ItemId)].Visible = false;
            detailView.Columns[nameof(childModel.ItemId)].Visible = false;

            detailView.Columns[nameof(childModel.SerialNumber)].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.SerialNumber)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.PurchaseApplicationId)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.RequestCategory)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ContractNo)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Name)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Code)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Category)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Brand)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Model)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Specification)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Dimension)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Unit)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.CurrentPurchasePrice)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Priority)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ProcessStatus)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.TotalApplied)]);

            detailView.Columns[nameof(childModel.CurrentPurchasePrice)].DisplayFormat.FormatType = FormatType.Numeric;
            detailView.Columns[nameof(childModel.CurrentPurchasePrice)].DisplayFormat.FormatString = "c2";
        }

        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
