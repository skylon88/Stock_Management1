using System.Windows.Forms;
using NewServices;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System;
using Core.Enum;
using System.Drawing;
using DevExpress.XtraSplashScreen;
using App.Helper;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Extensions;
using NewServices.Interfaces;
using NewServices.Models.采购部分;

namespace App.Views.采购部分
{
    public partial class PurchaseCtrl : CRUDCtrl
    {
        private IPurchaseService _purchaseService;
        private IManagementService _managementService;
        protected RequestCategoriesEnum _currentRequestCategory;
        private RepositoryItemLookUpEdit _positionRepItem;
        private RefreshHelper _refreshHelper;
        private PurchaseHeaderViewModel parentModel;
        private PurchaseViewModel childModel;

        public PurchaseCtrl(IPurchaseService purchaseService, IManagementService managementService)
        {
            _purchaseService = purchaseService;
            _managementService = managementService;

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            SetCurrentRequestCategory(RequestCategoriesEnum.材料需求);
            //this.gridControl1.DataSource = _purchaseService.GetAllPurchaseViewModels();
            //RefreshData();

            colApplicationNumber.Visible = false;
            colAuditDepart.Visible = false;
            RenderCommonHelper.SetColNotEditable(colPurchaseNumber);
            RenderCommonHelper.SetColNotEditable(colApplicationNumber);
            RenderCommonHelper.SetColNotEditable(colPONumber);
            RenderCommonHelper.SetColNotEditable(colSupplierName);
            RenderCommonHelper.SetColNotEditable(colPriority);
            RenderCommonHelper.SetColNotEditable(colPurchaseType);
            RenderCommonHelper.SetColNotEditable(colTotalPrice);
            RenderCommonHelper.SetColNotEditable(colCompletePercentage);
            RenderCommonHelper.SetColNotEditable(colDeliveryDate);
            RenderCommonHelper.SetColNotEditable(colCreateDate);
            RenderCommonHelper.SetColNotEditable(colUpdateDate);

            colCompletePercentage.DisplayFormat.FormatType = FormatType.Numeric;
            colCompletePercentage.DisplayFormat.FormatString = "P";
            //_positionRepItem = new RepositoryItemLookUpEdit();
            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.PurchaseNumber).ToString());
        }

        //修改Header
        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            PurchaseHeaderViewModel row = (PurchaseHeaderViewModel)this.gridView1.GetFocusedRow();
            _purchaseService.UpdatePurchaseHeader(row);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //更改Child View 名字
        private void gridView1_MasterRowGetRelationDisplayCaption(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "采购详细";
        }

        private void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            GridView detailView = e.View as GridView;
            detailView.OptionsSelection.MultiSelect = true;
            detailView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            if (detailView != null)
            {
                detailView.CustomRowCellEdit -= detailView_CustomRowCellEdit;
                detailView.CustomRowCellEdit += detailView_CustomRowCellEdit;

                detailView.RowCellStyle -= detailView1_RowCellStyle;
                detailView.RowCellStyle += detailView1_RowCellStyle;

                detailView.ShownEditor -= detailView_ShownEditor;
                detailView.ShownEditor += detailView_ShownEditor;

                detailView.SelectionChanged -= detailView_SelectionChanged;
                detailView.SelectionChanged += detailView_SelectionChanged;

                detailView.RowUpdated -= detailView_RowUpdated;
                detailView.RowUpdated += detailView_RowUpdated;

                detailView.MouseDown -= gridView1_MouseDown;
                detailView.MouseDown += gridView1_MouseDown;

                detailView.ValidateRow -= detailView_ValidateRow;
                detailView.ValidateRow += detailView_ValidateRow;

                detailView.InvalidRowException -= detailView_InvalidRowException;
                detailView.InvalidRowException += detailView_InvalidRowException;

                detailView.RowDeleting -= detailView1_RowDeleting;
                detailView.RowDeleting += detailView1_RowDeleting;

                detailView.RowDeleted -= detailView1_RowDeleted;
                detailView.RowDeleted += detailView1_RowDeleted;
            }

            //初始化供应商dropdownlist

            //_positionRepItem.Name = "DropDownForPosition";

            //_positionRepItem.DataSource = _managementService.GetPositionNameByCode(null);
            //gridControl1.RepositoryItems.Add(_positionRepItem);
            //GridColumn aColumn = detailView.Columns[nameof(model.Position)];
            //aColumn.ColumnEdit = _positionRepItem;

            var colReApplication = new GridColumn()
            {
                Name = "Action",
                Visible = true,
                VisibleIndex = 26,
                ColumnEdit = this.repositoryItemButtonEdit1

            };
            detailView.Columns.Add(colReApplication);
            detailView.Columns[nameof(childModel.PurchaseId)].Visible = false;
            detailView.Columns[nameof(childModel.SupplierCode)].Visible = false;
            detailView.Columns[nameof(childModel.PurchaseNumber)].Visible = false;
            detailView.Columns[nameof(childModel.ItemId)].Visible = false;
            detailView.Columns[nameof(childModel.IsPriceChange)].Visible = false;
            detailView.Columns[nameof(childModel.PoNumber)].Visible = false;
            detailView.Columns[nameof(childModel.IsDeleted)].Visible = false;
            detailView.Columns[nameof(childModel.RequestId)].Visible = false;
            detailView.Columns[nameof(childModel.Position)].Visible = false;

            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.SupplierCode)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ApplicationNumber)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Name)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Category)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Code)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Brand)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Model)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Specification)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Dimension)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Unit)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.DefaultPrice)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.PurchaseTotal)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.TotalPrice)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.AlreadyInStock)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Status)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Position)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.LastDeliveryDate)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.UpdateDate)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.PoNumber)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.IsDeleted)]);

            detailView.Columns[nameof(childModel.DefaultPrice)].DisplayFormat.FormatType = FormatType.Numeric;
            detailView.Columns[nameof(childModel.DefaultPrice)].DisplayFormat.FormatString = "c2";
            detailView.Columns[nameof(childModel.Price)].DisplayFormat.FormatType = FormatType.Numeric;
            detailView.Columns[nameof(childModel.Price)].DisplayFormat.FormatString = "c2";
            detailView.Columns[nameof(childModel.TotalPrice)].DisplayFormat.FormatType = FormatType.Numeric;
            detailView.Columns[nameof(childModel.TotalPrice)].DisplayFormat.FormatString = "c2";

            detailView.Columns.ForEach(x => x.OptionsFilter.AllowFilter = false);
        }

        private void gridView1_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();           
        }

        private void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {            
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            XtraInputBoxArgs args = new XtraInputBoxArgs();
            args.Caption = "请输入密码";
            args.Prompt = "密码";
            var password = XtraInputBox.Show(args)?.ToString();
            if (password == "admin")
            {
                var view = gridControl1.FocusedView as GridView;
                PurchaseViewModel row = (PurchaseViewModel) view.GetFocusedRow();
                switch (row.Status)
                {
                    case ProcessStatusEnum.采购入库:
                        MessageBox.Show("该项已经入库无法重新申请");
                        break;
                    case ProcessStatusEnum.申请审核中:
                        MessageBox.Show("该项已经在申请审核中");
                        break;
                    default:
                        if (MessageBox.Show("确定重新申请", "重新申请", MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
                            DialogResult.No)
                        {
                            row.IsDeleted = true;
                            _purchaseService.UpdatePurchase(row);
                            RefreshData(true);
                        }

                        break;
                }
            }
            else
            {
                MessageBox.Show("密码错误");
                return;
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
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

        public void RefreshData(bool isSaveState =false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _purchaseService.GetAllPurchaseViewModels(_currentRequestCategory);
            if (isSaveState) _refreshHelper.LoadViewInfo();
            SetupButtonStatus();
        }

        #region Checkbox Selection
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            SetupButtonStatus();
        }
        private void detailView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            SetupButtonStatus();
        }
        #endregion

        public IList<PurchaseViewModel> GetSelectedRows()
        {
            IList<PurchaseViewModel> purchaseViewModels = new List<PurchaseViewModel>();
            IList<GridView> detailViews = new List<GridView>();
            var parentRows = gridView1.RowCount;
            for (int p = 0; p < parentRows; p++)
            {
                var subView = gridView1.GetDetailView(p, 0) as GridView;
                if (subView != null)
                {
                    detailViews.Add(subView);
                }
            }
            foreach (var detail in detailViews)
            {
                var selectedRows = detail.GetSelectedRows();
                foreach (var s in selectedRows)
                {
                    purchaseViewModels.Add((PurchaseViewModel)detail.GetRow(s));
                }
            }
            return purchaseViewModels;
        }

        private void detailView_ShownEditor(object sender, System.EventArgs e)
        {
            GridView detailView = sender as GridView;
            if (detailView.FocusedColumn.FieldName != nameof(childModel.Position)) return;
            LookUpEdit editor = (LookUpEdit)gridView1.ActiveEditor;
            PurchaseViewModel item = (PurchaseViewModel)detailView.GetFocusedRow();
            var list = _managementService.GetPositionNameByCode(item.Code);
            editor.Properties.DataSource = list;
        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
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

        #region  GridView Color
        private void detailView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            e.Appearance.BackColor = Color.Beige;
            ProcessStatusEnum status = (ProcessStatusEnum)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Status));
            if (e.Column.FieldName == nameof(childModel.Status))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Status)).ToString(), out ProcessStatusEnum processStatus);
                RenderCommonHelper.SetStatusColor(processStatus, e);
            }

            if (e.Column.FieldName == nameof(childModel.Price))
            {
                bool isPriceChange = (bool)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.IsPriceChange));                
                if (isPriceChange)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                }
                
            }
        }
        #endregion

        #region SetupButtonStatus
        //全部确认Button
        private void SetupAllConfirmButtonStatus(Main main, IList<PurchaseViewModel> listOfPurchases)
        {
            if (listOfPurchases.Count == 0)
            {
                main.barButtonItem26.Enabled = false;
                return;
            }

            if (listOfPurchases.Count == 0)
            {
                main.barButtonItem26.Enabled = false;
                return; 
            }

            if (listOfPurchases.Any(x => x.Status == ProcessStatusEnum.采购入库 || x.Status == ProcessStatusEnum.采购完成))
            {
                main.barButtonItem26.Enabled = false;
                return;
            }

            if (listOfPurchases.All(x => x.Status != ProcessStatusEnum.采购中) ||
                listOfPurchases.All(x => x.Status != ProcessStatusEnum.退货中))
            {
                main.barButtonItem26.Enabled = true;
                return;
            }
 
        }

        //采购入库Button
        private void SetupInStockButtonStatus(Main main, IList<PurchaseViewModel> listOfPurchases)
        {
            if (listOfPurchases.Count == 0)
            {
                main.barButtonItem23.Enabled = false;
                return;
            }

            if (_currentRequestCategory != RequestCategoriesEnum.材料需求)
            {
                main.barButtonItem23.Enabled = false;
                return;
            }
            if (listOfPurchases.Any(x=>x.Status == ProcessStatusEnum.采购入库))
            {
                main.barButtonItem23.Enabled = false;
                return;
            }

            if (listOfPurchases.Any(x => x.Status != ProcessStatusEnum.采购完成))
            {
                main.barButtonItem23.Enabled = false;
                return;
            }

            var isDiffPurchaseNumber = listOfPurchases.Any(o => o.PurchaseNumber != listOfPurchases[0].PurchaseNumber);
            if (isDiffPurchaseNumber)
            {
                main.barButtonItem23.Enabled = false;
                return;
            }
            if (listOfPurchases.All(x => x.ReadyForInStock > 0))
            {
                main.barButtonItem23.Enabled = true;
                return;
            }
        }


        private void SetupButtonStatus()
        {
            var main = (Main)this.ParentForm;
            IList<PurchaseViewModel> Purchases = GetSelectedRows();
            if (main != null)
            {
                SetupAllConfirmButtonStatus(main, Purchases);
                SetupInStockButtonStatus(main, Purchases);
            }
        }
        #endregion


        protected virtual void detailView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView detailView = sender as GridView;
            Enum.TryParse(detailView.GetRowCellValue(e.RowHandle, nameof(childModel.Status)).ToString(), out ProcessStatusEnum processStatus);
            if (e.Column.FieldName == nameof(childModel.Price))
            {
                switch (processStatus)
                {
                    case ProcessStatusEnum.采购入库:
                        RenderCommonHelper.SetColNotEditable(e.Column);
                        break;
                    default:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                }
            }
            else if (e.Column.FieldName == nameof(childModel.ReadyForInStock))
            {
                switch (processStatus)
                {
                    case ProcessStatusEnum.采购入库:
                        RenderCommonHelper.SetColNotEditable(e.Column);
                        break;
                    default:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                }
            }
            
        }


        #region CURD
        private void detailView1_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            RefreshData(false);
        }

        private void detailView1_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            var MessageBoxResult = MessageBox.Show("是否删除该选项", "是否删除该选项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MessageBoxResult == DialogResult.Yes)
            {
                GridView detailView = sender as GridView;
                PurchaseViewModel row = (PurchaseViewModel)detailView.GetFocusedRow();
                if (row.Status != ProcessStatusEnum.采购中 && row.Status != ProcessStatusEnum.退货中)
                {
                    MessageBox.Show("该状态下无法删除");
                    e.Cancel = true;
                    return;
                }
                if (row.ReadyForInStock > 0)
                {
                    MessageBox.Show("到货数量大于零,无法删除");
                    e.Cancel = true;
                    return;
                }
                if (row.AlreadyInStock > 0)
                {
                    MessageBox.Show("已入库数量大于零,无法删除");
                    e.Cancel = true;
                    return;
                }
                _purchaseService.DeletePurchase(row);

                MessageBox.Show("该采购单项删除成功");
            }
            else if (MessageBoxResult != DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void detailView_RowUpdated(object sender, RowObjectEventArgs e)
        {
            var detailView = sender as GridView;
            PurchaseViewModel row = detailView.GetFocusedRow() as PurchaseViewModel;
            _purchaseService.UpdatePurchase(row);
            //RefreshData(true);
        }

        #endregion


        #region Validation
        private void detailView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            PurchaseViewModel model;
            GridView detailView = sender as GridView;
            detailView.ClearColumnErrors();
            GridColumn colReadyForInStock = detailView.Columns[nameof(model.ReadyForInStock)];
            Enum.TryParse(detailView.GetRowCellValue(e.RowHandle, nameof(model.Status)).ToString(), out ProcessStatusEnum status);
            if (status != ProcessStatusEnum.采购入库){
                double purchaseTotal = (double)detailView.GetRowCellValue(e.RowHandle, detailView.Columns[nameof(model.PurchaseTotal)]);
                double alreadyInStockTotal = (double)detailView.GetRowCellValue(e.RowHandle, detailView.Columns[nameof(model.AlreadyInStock)]);
                double readyForInStock = (double)detailView.GetRowCellValue(e.RowHandle, detailView.Columns[nameof(model.ReadyForInStock)]);
                if (readyForInStock < 0 || readyForInStock > purchaseTotal || readyForInStock > purchaseTotal - alreadyInStockTotal)
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns
                    detailView.SetColumnError(colReadyForInStock, "请输出正确的到货数量");

                }
            }
        }

        private void detailView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        #endregion

        public void SetCurrentRequestCategory(RequestCategoriesEnum requestCategory)
        {
            _currentRequestCategory = requestCategory;
        }

    }
}
