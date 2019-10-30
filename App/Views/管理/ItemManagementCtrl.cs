using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewServices;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using App.Helper;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors.Repository;
using System.Linq;
using Core.Enum;
using NewServices.Interfaces;
using NewServices.Models.管理;
using System.Drawing;
using System.Collections.Generic;

namespace App.Views.管理
{
    public sealed partial class ItemManagementCtrl : CRUDCtrl
    {
        private readonly IManagementService _managementService;
        private ItemViewModel parentModel;
        private PositionViewModel childModel;
        private readonly RefreshHelper _refreshHelper;
        private readonly RepositoryItemLookUpEdit _supplierRepItem;

        public ItemManagementCtrl(IManagementService managemntService) : base()
        {
            _managementService = managemntService;
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            colPackage.Visible = false;
            colDetailCategory.Visible = false;
            colAdjustCategory.Visible = false;
            colAttribute.Visible = false;
            colProperty.Visible = false;
            colMax.Visible = false;
            colMin.Visible = false;
            colCostCategory.Visible = false;
            colArrangeOrder.Visible = false;
            colArrangePosition.Visible = false;

            RenderCommonHelper.SetColNotEditable(colSerialNumber);
            RenderCommonHelper.SetColNotEditable(colCode);
            RenderCommonHelper.SetColNotEditable(colStatus);
            RenderCommonHelper.SetColNotEditable(colCategory);
            RenderCommonHelper.SetColNotEditable(colProjectCategory);
            RenderCommonHelper.SetColNotEditable(colSubCategory);
            RenderCommonHelper.SetColNotEditable(colBigCategory);
            RenderCommonHelper.SetColNotEditable(colSmallCategory);
            RenderCommonHelper.SetColNotEditable(colDetailCategory);
            RenderCommonHelper.SetColNotEditable(colAdjustCategory);
            RenderCommonHelper.SetColNotEditable(colAttribute);
            RenderCommonHelper.SetColNotEditable(colProperty);
            RenderCommonHelper.SetColNotEditable(colChineseName);
            RenderCommonHelper.SetColNotEditable(colEnglishName);
            RenderCommonHelper.SetColNotEditable(colBrand);
            RenderCommonHelper.SetColNotEditable(colModel);
            RenderCommonHelper.SetColNotEditable(colSpecification);
            RenderCommonHelper.SetColNotEditable(colDimension);
            RenderCommonHelper.SetColNotEditable(colUnit);
            RenderCommonHelper.SetColNotEditable(colPrice);
            RenderCommonHelper.SetColNotEditable(colTotalInStorage);
            RenderCommonHelper.SetColNotEditable(colDetail);
            RenderCommonHelper.SetColNotEditable(colMax);
            RenderCommonHelper.SetColNotEditable(colMin);
            RenderCommonHelper.SetColEditable(colFirstSupplier);
            RenderCommonHelper.SetColEditable(colSecondSupplier);
            RenderCommonHelper.SetColEditable(colThirdSupplier);
            RenderCommonHelper.SetColNotEditable(colCostCategory);
            RenderCommonHelper.SetColNotEditable(colComments);
            RenderCommonHelper.SetColNotEditable(colUpdateDate);

            colPrice.DisplayFormat.FormatType = FormatType.Numeric;
            colPrice.DisplayFormat.FormatString = "c2";


            //初始化供应商dropdownlist
            _supplierRepItem = new RepositoryItemLookUpEdit {Name = "DropDownForSupplier"};

            gridControl1.RepositoryItems.Add(_supplierRepItem);
            GridColumn aColumn1 = gridView1.Columns[nameof(parentModel.FirstSupplier)];
            GridColumn aColumn2 = gridView1.Columns[nameof(parentModel.SecondSupplier)];
            GridColumn aColumn3 = gridView1.Columns[nameof(parentModel.ThirdSupplier)];
            aColumn1.ColumnEdit = _supplierRepItem;
            aColumn2.ColumnEdit = _supplierRepItem;
            aColumn3.ColumnEdit = _supplierRepItem;


            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.ItemId).ToString());
        }

        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (sender is GridView gridView)
            {
                ItemViewModel row = gridView.GetFocusedRow() as ItemViewModel;
                _managementService.UpdateItem(row);
            }
        }

        //更改Child View 名字
        private void gridView1_MasterRowGetRelationDisplayCaption(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "库存详细";
        }

        private void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            PositionViewModel model;
            if (!(sender is GridView gridView)) return;
            var detailView = (GridView) gridView.GetDetailView(e.RowHandle, e.RelationIndex);
            detailView.ShowingEditor -= gridView1_ShowingEditor;
            detailView.ShowingEditor += gridView1_ShowingEditor;

            detailView.RowUpdated -= detailView_RowUpdated;
            detailView.RowUpdated += detailView_RowUpdated;

            detailView.RowDeleting -= detailView_RowDeleting;
            detailView.RowDeleting += detailView_RowDeleting;

            detailView.ValidateRow -= detailView_ValidateRow;
            detailView.ValidateRow += detailView_ValidateRow;

            detailView.InvalidRowException -= detailView_InvalidRowException;
            detailView.InvalidRowException += detailView_InvalidRowException;

            detailView.Columns[nameof(model.LatestInStockNumber)].Visible = false;

            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.PositionName)]);
            //RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.Total)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.LatestInStockNumber)]);
            RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.UpdateDate)]);RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(model.Comment)]);
        }

        private void detailView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            if (!(sender is GridView detailView)) return;
            detailView.ClearColumnErrors();
            var colTotal = detailView.Columns[nameof(childModel.Total)];
            var total = (double) detailView.GetRowCellValue(e.RowHandle, colTotal);
            var rowHandle = detailView.LocateByValue(nameof(childModel.PositionName), "Stage");
            detailView.GetRowCellValue(rowHandle, detailView.Columns[nameof(childModel.Total)]);
            if (total >= 0) return;
            e.Valid = false;
            detailView.SetColumnError(colTotal, "请输出正确的库存数量");
        }

        private void detailView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == nameof(parentModel.TotalInStorage))
            {
                var totalInStorage = currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.TotalInStorage));
                if (totalInStorage != null)
                {
                    if ((double)totalInStorage <= 0)
                    {
                        e.Appearance.BackColor = Color.DarkOrange;

                    }
                }

            }
        }

        private void detailView_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (sender is GridView detailView)
            {
                var row = detailView.GetFocusedRow() as PositionViewModel;
                SplashScreenManager.ShowDefaultWaitForm();
                IList<PositionViewModel> positionViewModels = null;
                var result = _managementService.TransferStorage(row, out positionViewModels);
            
                if (!result)
                {
                    MessageBox.Show("请输出正确的库存数量");
                    return;
                }
                RefreshOneItem(row.ItemId, positionViewModels);
            }

           
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void detailView_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            if (sender is GridView detailView)
            {
                var MessageBoxResult =
                    MessageBox.Show("是否删除该选项", "是否删除该选项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MessageBoxResult == DialogResult.Yes)
                {
                    PositionViewModel row = (PositionViewModel) detailView.GetFocusedRow();
                    if (row.Total > 0)
                    {
                        MessageBox.Show("当库存数量大于0时, 无法删除");
                        e.Cancel = true;
                        return;
                    }

                    if (row.PositionName == "Stage")
                    {
                        MessageBox.Show("无法删除Stage");
                        e.Cancel = true;
                        return;
                    }

                    var result = _managementService.DeletePosition(row);
                    if (!result)
                    {
                        MessageBox.Show("无法删除");
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
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedColumn.FieldName == nameof(childModel.Total))
            {
                var position = view.GetRowCellValue(view.FocusedRowHandle, nameof(childModel.PositionName));

                if (position == null)
                {
                    e.Cancel = true;
                }
                else
                {
                    string positionName = position.ToString();
                    if (positionName == "Stage")
                    {
                        e.Cancel = true;
                    }
                }
            }
                
        }



        public void RefreshData(bool isSaveState = false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _managementService.GetAllItems();
            this._supplierRepItem.DataSource = _managementService.GetAllSuppliers().Select(x => x.Name);
            if (isSaveState) _refreshHelper.LoadViewInfo();
        }

        public void RefreshOneItem(Guid itemId, IList<PositionViewModel> positionViewModels)
        {
            _refreshHelper.SaveViewInfo();
            var toUpdateDataSource = (IList<ItemViewModel>)this.gridControl1.DataSource;
            var selectedItem = toUpdateDataSource.Where(x => x.ItemId == itemId).FirstOrDefault().PositionViewModels = new BindingList<PositionViewModel>(positionViewModels);
            this.gridControl1.RefreshDataSource();
            _refreshHelper.LoadViewInfo();
        }

    }
}
