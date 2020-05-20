using System.Windows.Forms;
using NewServices;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System;
using Core.Enum;
using DevExpress.XtraSplashScreen;
using App.Helper;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Extensions;
using System.Drawing;
using DevExpress.Utils;
using NewServices.Interfaces;
using NewServices.Models.管理;
using NewServices.Models.需求部分;
using System.ComponentModel;

namespace App.Views.需求部分
{
    public partial class RequestHeadersCtrl : CRUDCtrl
    {
        protected IRequestService _requestService;
        protected IManagementService _managementService;
        protected RequestCategoriesEnum _currentRequestCategory;
        protected RefreshHelper _refreshHelper;
        protected RequestHeaderViewModel parentModel;
        protected RequestViewModel childModel;
        protected PositionViewModel subChildModel;

        public RequestHeadersCtrl(IRequestService requestService, IManagementService managementService)
        {
            _requestService = requestService;
            _managementService = managementService;
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            colCreateDate.Visible = false;
            colRequestCategory.Visible = false;
            RenderCommonHelper.SetColNotEditable(colRequestHeaderNumber);
            RenderCommonHelper.SetColNotEditable(colRequestCategory);
            RenderCommonHelper.SetColNotEditable(colPONumber);
            //RenderCommonHelper.SetColNotEditable(colContractId);
            //RenderCommonHelper.SetColNotEditable(colContractAddress);
            RenderCommonHelper.SetColNotEditable(colTotal);
            RenderCommonHelper.SetColNotEditable(colStatus);
            RenderCommonHelper.SetColNotEditable(colLockStatus);
            RenderCommonHelper.SetColNotEditable(colCreateDate);
            RenderCommonHelper.SetColNotEditable(colUpdateDate);

            colCompletePercentage.DisplayFormat.FormatType = FormatType.Numeric;colCompletePercentage.DisplayFormat.FormatString = "P";

            _refreshHelper = new RefreshHelper(gridView1, nameof(parentModel.RequestHeaderNumber).ToString());
        }

        //修改Header
        private void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            RequestHeaderViewModel row = (RequestHeaderViewModel)this.gridView1.GetFocusedRow();
            _requestService.UpdateRequestHeader(row);
            SplashScreenManager.CloseDefaultWaitForm();
        }


        //删除parent view
        public override void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                IList<RequestViewModel> listOfRequests = GetSelectedRows();
                if (listOfRequests.Count > 0)
                {
                    var MessageBoxResult = MessageBox.Show("彻底删除(Yes). 取消(Cancel)", "是否删除该选项", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (MessageBoxResult == DialogResult.Yes)
                    {
                        string returnMessage = string.Empty;
                        SplashScreenManager.ShowForm(typeof(WaitForm));
                        int i = 1;
                        foreach (var row in listOfRequests)
                        {
                            var result = _requestService.DeleteRequest(row, out returnMessage);
                            if (!result)
                            {

                                e.Handled = true;
                                break;
                            }
                            returnMessage += i + ":" + returnMessage + "\n";
                            i++;
                        }
                        MessageBox.Show(returnMessage);
                        SplashScreenManager.CloseForm();
                        RefreshData(true);
                    }
                    else if (MessageBoxResult != DialogResult.No)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }

            }
            if (e.Button.ButtonType == NavigatorButtonType.EndEdit)
            {
                //RefreshData(true);
            }
            if (e.Button.ButtonType == NavigatorButtonType.CancelEdit)
            {
                RefreshData(true);
            }
        }

        //修改detail
        protected void detailView_RowUpdated(object sender, RowObjectEventArgs e)
        {
            
            var detailView = sender as GridView;
            RequestViewModel row = (RequestViewModel)detailView.GetFocusedRow();
            int result = _requestService.UpdateRequest(row);
            if (result == 2)
            {
                //SplashScreenManager.ShowDefaultWaitForm();
                RefreshData(true);
                //SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        //更改Child View 名字
        private void gridView1_MasterRowGetRelationDisplayCaption(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "需求详细";
        }

        //更改Child View 名字
        private void detailView1_MasterRowGetRelationDisplayCaption(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "库存信息";
        }

        //Register detailView
        protected virtual void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            GridView detailView = e.View as GridView;
            if (detailView.Columns[0].Name == "colPositionName")
            {
                detailView.OptionsSelection.MultiSelect = false;
                if (detailView != null)
                {
                    detailView.RowUpdated -= positionView_RowUpdated;
                    detailView.RowUpdated += positionView_RowUpdated;

                    detailView.ShowingEditor -= gridView1_ShowingEditor;
                    detailView.ShowingEditor += gridView1_ShowingEditor;

                    detailView.ValidateRow -= positionView_ValidateRow;
                    detailView.ValidateRow += positionView_ValidateRow;
                }

                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(subChildModel.PositionName)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(subChildModel.UpdateDate)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(subChildModel.LatestInStockNumber)]);
            }

            else
            {
                detailView.OptionsSelection.MultiSelect = true;
                detailView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

                detailView.MasterRowGetRelationDisplayCaption -= detailView1_MasterRowGetRelationDisplayCaption;
                detailView.MasterRowGetRelationDisplayCaption += detailView1_MasterRowGetRelationDisplayCaption;

                detailView.RowCellStyle -= detailView1_RowCellStyle;
                detailView.RowCellStyle += detailView1_RowCellStyle;

                detailView.CustomRowCellEdit -= detailView_CustomRowCellEdit;
                detailView.CustomRowCellEdit += detailView_CustomRowCellEdit;

                detailView.SelectionChanged -= detailView_SelectionChanged;
                detailView.SelectionChanged += detailView_SelectionChanged;

                detailView.RowUpdated -= detailView_RowUpdated;
                detailView.RowUpdated += detailView_RowUpdated;

                detailView.MasterRowExpanding -= detailView_MasterRowExpanding;
                detailView.MasterRowExpanding += detailView_MasterRowExpanding;

                detailView.MasterRowExpanded -= detailView_MasterRowExpanded;
                detailView.MasterRowExpanded += detailView_MasterRowExpanded;

                detailView.ValidateRow -= detailView_ValidateRow;
                detailView.ValidateRow += detailView_ValidateRow;

                detailView.InvalidRowException -= detailView_InvalidRowException;
                detailView.InvalidRowException += detailView_InvalidRowException;


                //Invisable 
                detailView.Columns[nameof(childModel.RequestId)].Visible = false;
                detailView.Columns[nameof(childModel.ItemId)].Visible = false;
                detailView.Columns[nameof(childModel.PoNumber)].Visible = false;
                detailView.Columns[nameof(childModel.RequestNumber)].Visible = false;
                detailView.Columns[nameof(childModel.RequestCategory)].Visible = false;
                detailView.Columns[nameof(childModel.Max)].Visible = false;
                detailView.Columns[nameof(childModel.AvailableInStorage)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                detailView.Columns[nameof(childModel.SerialNumber)].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                ////Editable 
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.SerialNumber)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.RequestNumber)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.RequestCategory)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.TotalInStorage)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.AvailableInStorage)]);


                RenderCommonHelper.SetColEditable(detailView.Columns[nameof(childModel.ToInStockTotal)]);
                RenderCommonHelper.SetColEditable(detailView.Columns[nameof(childModel.ToOutStockTotal)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.OutStockTotal)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.InStockTotal)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ToDestoryTotal)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.DestoriedTotal)]);

                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.ToApplyTotal)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Name)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Code)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Unit)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.Status)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.UpdateDate)]);
                RenderCommonHelper.SetColNotEditable(detailView.Columns[nameof(childModel.PoNumber)]);




                detailView.Columns.ForEach(x => x.OptionsFilter.AllowFilter = false);
            }
        }

        protected void gridView1_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
        }

        //隐藏Child view column
        protected virtual void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            SplashScreenManager.CloseDefaultWaitForm();
        }

        protected virtual void detailView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView detailView = sender as GridView;
            Enum.TryParse(detailView.GetRowCellValue(e.RowHandle, nameof(parentModel.Status)).ToString(), out ProcessStatusEnum processStatus);
            Enum.TryParse(detailView.GetRowCellValue(e.RowHandle, nameof(parentModel.RequestCategory)).ToString(), out RequestCategoriesEnum requestCategory);
            if (e.Column.FieldName == nameof(childModel.Total))
            {
                switch (processStatus)
                {
                    case ProcessStatusEnum.需求建立:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    //根据5月2 号的讨论, 当状态为 采购入库 或 已出库 需要修改需求数量
                    case ProcessStatusEnum.采购入库:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    case ProcessStatusEnum.已出库:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    default:
                        RenderCommonHelper.SetColNotEditable(e.Column);
                        break;
                }
            }

            if (e.Column.FieldName == nameof(childModel.LockStatus))
            {
                switch (processStatus)
                {
                    case ProcessStatusEnum.需求建立:
                        RenderCommonHelper.SetColEditable(e.Column);
                        break;
                    default:
                        RenderCommonHelper.SetColNotEditable(e.Column);
                        break;
                }
            }


            if (e.Column.FieldName == nameof(childModel.PositionName))
            {
                RequestViewModel model = (RequestViewModel)detailView.GetRow(e.RowHandle);
                RepositoryItemLookUpEdit positionRepItem = new RepositoryItemLookUpEdit();
                positionRepItem.Name = "DropDownForPosition";
                positionRepItem.NullText = "";
                positionRepItem.ValueMember = nameof(childModel.PositionName);
                positionRepItem.DisplayMember = nameof(childModel.PositionName);
                positionRepItem.DataSource = model.PositionViewModels.ToList();
                positionRepItem.PopupWidth = 610;
                //positionRepItem.PopulateColumns();
                //positionRepItem.Columns[nameof(subChildModel.UpdateDate)].Visible = false;
                //positionRepItem.Columns[nameof(subChildModel.LatestInStockNumber)].Visible = false;
                e.RepositoryItem = positionRepItem;
                //positionRepItem.EditValueChanged -= RepositoryItemLookUpEdit1_EditValueChanged;
                //positionRepItem.EditValueChanged += RepositoryItemLookUpEdit1_EditValueChanged;
            }
        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedColumn.FieldName == nameof(childModel.Total))
            {
                string positionName = view.GetRowCellValue(view.FocusedRowHandle, nameof(childModel.PositionName)).ToString();
                if (positionName == "Stage")
                {
                    e.Cancel = true;
                }
            }

        }


        #region Detail Position Expand
        //Expanding Detail View
        protected void detailView_MasterRowExpanding(object sender, MasterRowCanExpandEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
        }

        //Expand Detail View
        public virtual void detailView_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //修改库位 Validation
        private void positionView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            PositionViewModel model;
            GridView detailView = sender as GridView;
            detailView.ClearColumnErrors();
            GridColumn colTotal = detailView.Columns[nameof(model.Total)];
            double total = (double)detailView.GetRowCellValue(e.RowHandle, colTotal);
            int rowHandle = detailView.LocateByValue(nameof(model.PositionName), "Stage");
            double stageTotal = (double)detailView.GetRowCellValue(rowHandle, detailView.Columns[nameof(model.Total)]);
            if (total < 0)
            {
                e.Valid = false;
                detailView.SetColumnError(colTotal, "请输出正确的库存数量");
            }
        }

        //修改库位
        private void positionView_RowUpdated(object sender, RowObjectEventArgs e)
        {
            var positionView = sender as GridView;
            PositionViewModel row = positionView.GetFocusedRow() as PositionViewModel;
            IList<PositionViewModel> positionViewModels = null;
            bool result = _managementService.TransferStorage(row, out positionViewModels);

            if (!result)
            {
                MessageBox.Show("请输出正确的库存数量");
            }
            RefreshData(true);
        }
        #endregion
        
        #region Checkbox Selection
        protected void detailView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            SetupButtonStatus();
        }
        #endregion

        #region SetupButtonStatus

        //采购申请Button
        private void SetupApplicationButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            bool IsDiffRequestNumber = listOfRequests.Any(o => o.RequestNumber != listOfRequests[0].RequestNumber);
            if (listOfRequests != null && listOfRequests.Count > 0 && !IsDiffRequestNumber)
            {
                if (_currentRequestCategory != RequestCategoriesEnum.材料需求 &&
                    _currentRequestCategory != RequestCategoriesEnum.工程车补给 &&
                    _currentRequestCategory != RequestCategoriesEnum.员工补给 &&
                    _currentRequestCategory != RequestCategoriesEnum.购入需求 )
                {
                    main.barButtonItem8.Enabled = false;
                    return;
                }
                if (listOfRequests.Any(x => x.Status != ProcessStatusEnum.需求建立))
                {
                    main.barButtonItem8.Enabled = false;
                    return;
                }
                if (listOfRequests.Any(x => x.LockStatus == LockStatusEnum.未准备))
                {
                    main.barButtonItem8.Enabled = false;
                    return;
                }
                main.barButtonItem8.Enabled = true;
                return;
            }
            main.barButtonItem8.Enabled = false;
        }

        //出库Button
        private void SetupOutStockButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.材料需求)
                {
                    main.barButtonItem9.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x=>x.ToOutStockTotal > 0))
                {
                    main.barButtonItem9.Enabled = true;
                    return;
                }
            }
            main.barButtonItem9.Enabled = false;
        }

        

        //工程车维修入库button
        private void SetupFixInStockButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if(_currentRequestCategory != RequestCategoriesEnum.工程车维修)
                {
                    main.barButtonItem10.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.需求建立))
                {
                    main.barButtonItem10.Enabled = true;
                    return;
                }
            }
            main.barButtonItem10.Enabled = false;
        }

        //维修开始button
        private void SetupFixStartButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.工程车维修 && _currentRequestCategory != RequestCategoriesEnum.工具维修)
                {
                    main.barButtonItem34.Enabled = false;
                    return;
                }

                if (listOfRequests.Any(x => x.AvailableInStorage <= 0))
                {
                    main.barButtonItem34.Enabled = false;
                    return;
                }
                if (listOfRequests.Count > 0 && 
                    (listOfRequests.All(x => x.Status == ProcessStatusEnum.需求建立) &&
                    listOfRequests.All(x => x.RequestCategory == RequestCategoriesEnum.工具维修)
                    ||
                    listOfRequests.All(x => x.Status == ProcessStatusEnum.工程车维修入库) &&
                    listOfRequests.All(x => x.RequestCategory == RequestCategoriesEnum.工程车维修)
                    ))
                {
                    main.barButtonItem34.Enabled = true;
                    return;
                }
                //if (listOfRequests.Any(x => x.ToInStockTotal <= 0))
                //{
                //    main.barButtonItem34.Enabled = false;
                //    return;
                //}
            }
            main.barButtonItem34.Enabled = false;
        }

        //维修结束button
        private void SetupFixFinishButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.工程车维修 && _currentRequestCategory != RequestCategoriesEnum.工具维修)
                {
                    main.barButtonItem35.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.维修中))
                {
                    main.barButtonItem35.Enabled = true;
                    return;
                }
            }
            main.barButtonItem35.Enabled = false;
        }
        //工程车维修出库Button
        private void SetupFixOutStockButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.工程车维修)
                {
                    main.barButtonItem11.Enabled = false;
                    return;
                }       
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.维修完成))
                {
                    main.barButtonItem11.Enabled = true;
                    return;
                }
                //if (listOfRequests.Any(x => x.ToInStockTotal <= 0))
                //{
                //    main.barButtonItem11.Enabled = false;
                //    return;
                //}
            }
            main.barButtonItem11.Enabled = false;
        }
        //报废Button
        private void SetupDestoryButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.工程车维修 && _currentRequestCategory != RequestCategoriesEnum.工具维修)
                {
                    main.barButtonItem12.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.工程车维修入库) || listOfRequests.All(x => x.Status == ProcessStatusEnum.维修完成))
                {
                    main.barButtonItem12.Enabled = true;
                    return;
                }
            }
            main.barButtonItem12.Enabled = false;
        }

        //借出Button
        private void SetupLoanButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.工具借出)
                {
                    main.barButtonItem29.Enabled = false;
                    return;
                }
                if (listOfRequests.Any(x => x.TotalInStorage <= 0) || listOfRequests.Any(x => x.ToOutStockTotal == 0))
                {
                    main.barButtonItem29.Enabled = false;
                    return;
                }

                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.需求建立))
                {
                    main.barButtonItem29.Enabled = true;
                    return;
                }              
            }
            main.barButtonItem29.Enabled = false;
        }

        //归还Button
        private void SetupReturnButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.工具借出)
                {
                    main.barButtonItem38.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.借出出库))
                {
                    main.barButtonItem38.Enabled = true;
                    return;
                }
            }
            main.barButtonItem38.Enabled = false;
        }

        //补给Button
        private void SetupFillInButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.员工补给 && _currentRequestCategory != RequestCategoriesEnum.工程车补给)
                {
                    main.barButtonItem28.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.ToOutStockTotal > 0))
                {
                    main.barButtonItem28.Enabled = true;
                    return;
                }
                //if (listOfRequests.Count > 0)
                //{
                //    main.barButtonItem28.Enabled = true;
                //    return;
                //}
            }
            main.barButtonItem28.Enabled = false;
        }

        //退回Button
        private void SetupReturnItemButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.物品退回)
                {
                    main.barButtonItem36.Enabled = false;
                    return;
                }
                
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.需求建立))
                {
                    main.barButtonItem36.Enabled = true;
                    return;
                }
            }
            main.barButtonItem36.Enabled = false;
        }

        //退货申请Button
        private void SetupRefundApplyItemButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.采购退货)
                {
                    main.barButtonItem37.Enabled = false;
                    return;
                }
                if (listOfRequests.Any(x => x.LockStatus == LockStatusEnum.未准备))
                {
                    main.barButtonItem37.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.需求建立))
                {
                    main.barButtonItem37.Enabled = true;
                    return;
                }
                //if (listOfRequests.Any(x => x.TotalApply <= 0))
                //{
                //    main.barButtonItem37.Enabled = false;
                //    return;
                //}
            }
            main.barButtonItem37.Enabled = false;
        }

        //退货Button
        private void SetupRefundItemButtonStatus(Main main, IList<RequestViewModel> listOfRequests)
        {
            if (IsSelectedRequestNumberSame(listOfRequests))
            {
                if (_currentRequestCategory != RequestCategoriesEnum.采购退货)
                {
                    main.barButtonItem48.Enabled = false;
                    return;
                }
                if (listOfRequests.All(x => x.Status == ProcessStatusEnum.退货申请完成))
                {
                    main.barButtonItem48.Enabled = true;
                    return;
                }
            }
            main.barButtonItem48.Enabled = false;
        }

        private void SetupButtonStatus()
        {
            var main = (Main)this.ParentForm;
            IList<RequestHeaderViewModel> listOfRequestHeader = GetSelectedHeaderRows();
            IList<RequestViewModel> listOfRequests = GetSelectedRows();
            if (main != null)
            {
                SetupApplicationButtonStatus(main, listOfRequests);
                SetupOutStockButtonStatus(main, listOfRequests);
                SetupFixInStockButtonStatus(main, listOfRequests);
                SetupFixOutStockButtonStatus(main, listOfRequests);
                SetupFixStartButtonStatus(main, listOfRequests);
                SetupFixFinishButtonStatus(main, listOfRequests);
                SetupDestoryButtonStatus(main, listOfRequests);
                SetupLoanButtonStatus(main, listOfRequests);
                SetupReturnButtonStatus(main, listOfRequests);
                SetupFillInButtonStatus(main, listOfRequests);
                SetupReturnItemButtonStatus(main, listOfRequests);
                SetupRefundApplyItemButtonStatus(main, listOfRequests);
                SetupRefundItemButtonStatus(main, listOfRequests);
            }


        }
        #endregion

        #region Validation
        protected virtual void detailView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            RequestViewModel model;
            GridView detailView = sender as GridView;
            detailView.ClearColumnErrors();
            //GridColumn colTotalInDeliery = detailView.Columns[nameof(model.OutStockTotal)];
            //int intotalInStock = (int)detailView.GetRowCellValue(e.RowHandle, detailView.Columns[nameof(model.TotalInStorage)]);
            //int readyForInStock = (int)detailView.GetRowCellValue(e.RowHandle, detailView.Columns[nameof(model.OutStockTotal)]);
            //if (readyForInStock < 0 || readyForInStock > intotalInStock)
            //{
            //    e.Valid = false;
            //    //Set errors with specific descriptions for the columns
            //    detailView.SetColumnError(colTotalInDeliery, "请输出正确的到货数量");

            //}
        }

        private void detailView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        #endregion

        #region  GridView Color
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == nameof(parentModel.Status))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.Status)).ToString(), out ProcessStatusEnum processStatus);
                RenderCommonHelper.SetStatusColor(processStatus, e);
                
            }
            if (e.Column.FieldName == nameof(parentModel.RequestCategory))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.RequestCategory)).ToString(), out RequestCategoriesEnum requestCategories);
                RenderCommonHelper.SetCategoryColor(requestCategories, e);

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
            if (e.Column.FieldName == nameof(parentModel.LockStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.LockStatus)).ToString(), out LockStatusEnum lockStatus);
                if (lockStatus == LockStatusEnum.已准备)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else e.Appearance.BackColor = Color.LightYellow;

            }

        }


        
        protected virtual void detailView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            e.Appearance.BackColor = Color.Beige;
            if (e.Column.FieldName == nameof(childModel.Status))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(childModel.Status)).ToString(), out ProcessStatusEnum processStatus);
                RenderCommonHelper.SetStatusColor(processStatus, e);
            }
            if (e.Column.FieldName == nameof(childModel.ToOutStockTotal))
            {
                var toOutStockTotal = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.ToOutStockTotal));
                e.Appearance.BackColor = toOutStockTotal > 0 ? Color.LawnGreen : Color.DarkOrange;
            }
            if (e.Column.FieldName == nameof(childModel.AvailableInStorage))
            {var maxDisplay = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.AvailableInStorage));
                //e.Appearance.BackColor = maxDisplay.Contains("+") ? Color.LawnGreen : Color.DarkOrange;
                e.Appearance.BackColor = maxDisplay> 0? Color.LawnGreen : Color.DarkOrange;
            }
            if (e.Column.FieldName == nameof(childModel.DestoriedTotal))
            {
                var destoriedTotal = (double)currentView.GetRowCellValue(e.RowHandle, nameof(childModel.DestoriedTotal));
                if (destoriedTotal > 0)
                {
                    e.Appearance.BackColor = Color.Red;
                }                
            }

            if (e.Column.FieldName == nameof(childModel.LockStatus))
            {
                Enum.TryParse(currentView.GetRowCellValue(e.RowHandle, nameof(parentModel.LockStatus)).ToString(), out LockStatusEnum lockStatus);
                if (lockStatus == LockStatusEnum.已准备)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else e.Appearance.BackColor = Color.LightYellow;

            }
        }
        #endregion

        #region Call from another controller
        public IList<RequestViewModel> GetSelectedRows()
        {
            IList<RequestViewModel> requestViewModels = new List<RequestViewModel>();
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
                    requestViewModels.Add((RequestViewModel)detail.GetRow(s));
                }
            }
            return requestViewModels;
        }

        public virtual bool IsValidated(IList<RequestViewModel> items)
        {
            //if (items == null || items.Count == 0)
            //{
            //    return false;
            //}
            //else if (items.Any(x => x.ToInStockTotal < 0))
            //{
            //    return false;
            //}
            //else return true;
            return true;
        }


        public IList<RequestHeaderViewModel> GetSelectedHeaderRows()
        {
            IList<RequestHeaderViewModel> requestHeaderViewModels = new List<RequestHeaderViewModel>();
            var parentRows = gridView1.GetSelectedRows();
            foreach (var item in parentRows)
            {
                requestHeaderViewModels.Add((RequestHeaderViewModel)gridView1.GetRow(item));
            }
            return requestHeaderViewModels;
        }

        public void SetCurrentRequestCategory(RequestCategoriesEnum requestCategory)
        {
            _currentRequestCategory = requestCategory;
        }

        public void RefreshData(bool isSaveState = false)
        {
            if (isSaveState) _refreshHelper.SaveViewInfo();
            this.gridControl1.DataSource = _requestService.GetAllRequestHeaderByCategory(_currentRequestCategory, null);
            if (isSaveState) _refreshHelper.LoadViewInfo();
            SetupButtonStatus();
        }

        #endregion

        #region Helper Functions

        private bool IsSelectedRequestNumberSame(IList<RequestViewModel> listOfRequests)
        {
            if (listOfRequests != null && listOfRequests.Count > 0)
            {
                bool IsDiffRequestNumber = listOfRequests.Any(o => o.RequestNumber != listOfRequests[0].RequestNumber);
                return !IsDiffRequestNumber;
            }
            return false;
        }

        #endregion


    }
}
