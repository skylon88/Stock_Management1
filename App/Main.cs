using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Helper;
using App.Views;
using App.Views.入库部分;
using App.Views.出库部分;
using App.Views.管理;
using App.Views.采购部分;
using App.Views.需求部分;
using Core.Enum;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using NewServices;
using NewServices.Interfaces;
using NewServices.Models.需求部分;

namespace App
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly IRequestService _requestService;
        private readonly IManagementService _managementService;
        private readonly IPurchaseService _purchaseService;
        private readonly IStockService _stockService;
        private readonly MaterialRequestCtrl _materialRequestCtrl;
        private readonly LoanRequestCtrl _loanRequestCtrl;
        private readonly ToolRequestCtrl _toolRequestCtrl;
        private readonly CarRequestCtrl _carRequestCtrl;
        private readonly CarFixRequestCtrl _carfixRequestCtrl;
        private readonly EmployeeRequestCtrl _employeeRequestCtrl;
        private readonly ReturnRequestCtrl _returnRequestCtrl;
        private readonly RefundRequestCtrl _refundRequestCtrl;
        private readonly InstockRequestCtrl _instockRequestCtrl;
        private readonly ContractManagementCtrl _contractCtrl;
        private readonly PurchaseApplicationCtrl _purchaseApplicationCtrl;
        private readonly RefundApplicationCtrl _refundApplicationCtrl;
        private readonly ItemManagementCtrl _itemManagementCtrl;
        private readonly PurchaseCtrl _purchaseCtrl;
        private readonly RefundCtrl _refundCtrl;
        private readonly InStockCtrl _inStockCtrl;
        private readonly InDeliveryCtrl _inDeliveryCtrl;
        private SupplierManagementCtrl _supplierManagementCtrl;

        public Main(IRequestService requestService, IPurchaseService purchaseService, IManagementService managementService, IStockService stockService)
        {
            _requestService = requestService;
            _managementService = managementService;
            _purchaseService = purchaseService;
            _stockService = stockService;
            InitializeComponent();

            _materialRequestCtrl = DependencyInjector.Retrieve<MaterialRequestCtrl>();
            _loanRequestCtrl = DependencyInjector.Retrieve<LoanRequestCtrl>();
            _toolRequestCtrl = DependencyInjector.Retrieve<ToolRequestCtrl>();
            _carRequestCtrl = DependencyInjector.Retrieve<CarRequestCtrl>();
            _carfixRequestCtrl = DependencyInjector.Retrieve<CarFixRequestCtrl>();
            _employeeRequestCtrl = DependencyInjector.Retrieve<EmployeeRequestCtrl>();
            _returnRequestCtrl = DependencyInjector.Retrieve<ReturnRequestCtrl>();
            _refundRequestCtrl = DependencyInjector.Retrieve<RefundRequestCtrl>();
            _instockRequestCtrl = DependencyInjector.Retrieve<InstockRequestCtrl>();

            _contractCtrl = DependencyInjector.Retrieve<ContractManagementCtrl>();
            _purchaseApplicationCtrl = DependencyInjector.Retrieve<PurchaseApplicationCtrl>();
            _refundApplicationCtrl = DependencyInjector.Retrieve<RefundApplicationCtrl>();
            _itemManagementCtrl = DependencyInjector.Retrieve<ItemManagementCtrl>();
            _supplierManagementCtrl = DependencyInjector.Retrieve<SupplierManagementCtrl>();
            _purchaseCtrl = DependencyInjector.Retrieve<PurchaseCtrl>();
            _refundCtrl = DependencyInjector.Retrieve<RefundCtrl>();
            _inStockCtrl = DependencyInjector.Retrieve<InStockCtrl>();
            _inDeliveryCtrl = DependencyInjector.Retrieve<InDeliveryCtrl>();

            this.xtraTabControl1.Text = TabCategory.需求.ToString();
            this.xtraTabControl2.Text = TabCategory.采购申请.ToString();
            this.xtraTabControl3.Text = TabCategory.采购单.ToString();
            this.xtraTabControl4.Text = TabCategory.入库.ToString();
            this.xtraTabControl5.Text = TabCategory.出库.ToString();
            this.xtraTabControl6.Text = TabCategory.管理.ToString();

            InitializeButton();


        }

        private void InitializeButton()
        {
            barButtonItem8.Enabled = false; //采购申请
            barButtonItem9.Enabled = false; //出库
            barButtonItem10.Enabled = false; //工程车维修入库
            barButtonItem34.Enabled = false; //维修开始
            barButtonItem35.Enabled = false; //维修结束
            barButtonItem11.Enabled = false; //工程车维修出库
            barButtonItem12.Enabled = false; //报废
            barButtonItem28.Enabled = false; //补给
            barButtonItem29.Enabled = false; //借出
            barButtonItem38.Enabled = false; //归还
            barButtonItem36.Enabled = false; //退回
            barButtonItem37.Enabled = false; //退货

            barButtonItem15.Enabled = false; //审核
            barButtonItem18.Enabled = false; //生成采购单
            barButtonItem46.Enabled = false; //生成退货单
            barButtonItem48.Enabled = false; //退货出库

            barButtonItem23.Enabled = false; //采购入库
            barButtonItem26.Enabled = false; //全部确认

            barButtonItem43.Enabled = false;

        }

        #region 需求类

        //材料需求
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _materialRequestCtrl.RefreshData();
            AddControlToPage(_materialRequestCtrl, TabCategory.需求, RequestCategoriesEnum.材料需求.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //工具借出
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _loanRequestCtrl.RefreshData();
            AddControlToPage(_loanRequestCtrl, TabCategory.需求, RequestCategoriesEnum.工具借出.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //物品维修
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _toolRequestCtrl.RefreshData();
            AddControlToPage(_toolRequestCtrl, TabCategory.需求, RequestCategoriesEnum.工具维修.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //工程车维修
        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _carfixRequestCtrl.RefreshData();
            AddControlToPage(_carfixRequestCtrl, TabCategory.需求, RequestCategoriesEnum.工程车维修.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //工程车补给
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _carRequestCtrl.RefreshData();
            AddControlToPage(_carRequestCtrl, TabCategory.需求, RequestCategoriesEnum.工程车补给.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //员工补给
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _employeeRequestCtrl.RefreshData();
            AddControlToPage(_employeeRequestCtrl, TabCategory.需求, RequestCategoriesEnum.员工补给.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //物品退回
        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _returnRequestCtrl.RefreshData();
            AddControlToPage(_returnRequestCtrl, TabCategory.需求, RequestCategoriesEnum.物品退回.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }      

        //采购退货
        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _refundRequestCtrl.RefreshData();
            AddControlToPage(_refundRequestCtrl, TabCategory.需求, RequestCategoriesEnum.采购退货.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //购入需求
        private void BarButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _instockRequestCtrl.RefreshData();
            AddControlToPage(_instockRequestCtrl, TabCategory.需求, RequestCategoriesEnum.购入需求.ToString());
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //上传物品需求表
        private async void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var filenames = CustomizeHelper.LoadFolder();
            int result = 0;
            if (filenames == null)
            {
                return;
            }
            else
            {
                
                string returnMessage = string.Empty;
                int count = filenames.Length;
                int i = 0;
                int increment_interval = 100 / count;
                await Task.Run(() =>
                {
                    SplashScreenManager.ShowForm(typeof(WaitForm));
                    foreach (var filename in filenames)
                    {
                        result = _requestService.Upload(filename, out returnMessage);
                        i++;
                        SplashScreenManager.Default.SendCommand(WaitForm.WaitFormCommand.SetProgress, increment_interval * i);
                    }
                });
                SplashScreenManager.CloseForm();
                switch (result)
                {
                    case 1:
                        break;
                    case 2:
                        MessageBox.Show("以下需求订单已经存在 :" + returnMessage);
                        break;
                    case 3:
                        MessageBox.Show("以下Item Id 不存在 :" + returnMessage);
                        break;
                    default:
                        MessageBox.Show(returnMessage);
                        break;
                }
                
                _materialRequestCtrl.RefreshData();
            }
        }

        //采购申请->采购申请汇总
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _purchaseApplicationCtrl.RefreshData(false);
            AddControlToPage(_purchaseApplicationCtrl, TabCategory.采购申请, ApplicationCategoriesEnum.采购申请汇总.ToString());
        }

        //采购申请->退货申请汇总
        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            _refundApplicationCtrl.RefreshData(false);
            AddControlToPage(_refundApplicationCtrl, TabCategory.采购申请, ApplicationCategoriesEnum.退货申请汇总.ToString());
        }

        //采购单->采购单汇总
        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            _purchaseCtrl.RefreshData(false);
            AddControlToPage(_purchaseCtrl, TabCategory.采购单, PurchaseCategoriesEnum.采购单.ToString());
        }

        //采购单->退货单汇总
        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            _refundCtrl.RefreshData(false);
            AddControlToPage(_refundCtrl, TabCategory.采购单, PurchaseCategoriesEnum.退货单.ToString());
        }

        #endregion

        #region 采购申请

        //需求准备(Lock)
        private void BarButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl = (RequestHeadersCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
            var items = selectedCtrl.GetSelectedRows().ToList();
            SplashScreenManager.ShowDefaultWaitForm();
            foreach (var item in items)
            {
                if (item.LockStatus == LockStatusEnum.未准备)
                {
                    item.LockStatus = LockStatusEnum.已准备;
                    _requestService.UpdateRequest(item);
                }
            }
            SplashScreenManager.CloseDefaultWaitForm();
            selectedCtrl.RefreshData(true);
        }

        //采购申请
        private async void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string PONumber = string.Empty;
            string temp_applicationNumber = string.Empty;

            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.材料需求:
                    selectedCtrl = (RequestHeadersCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.购入需求:
                    selectedCtrl = (RequestHeadersCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.工程车补给:
                    selectedCtrl = (CarRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.员工补给:
                    selectedCtrl = (EmployeeRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            IList<RequestViewModel> items = new List<RequestViewModel>();
            var masters = selectedCtrl.GetSelectedHeaderRows();
            if (masters.Count > 0)
            {
                items = masters.SelectMany(x => x.RequestViewModels.Where(s=>s.Status == ProcessStatusEnum.需求建立 && s.ToApplyTotal > 0)).ToList();
            }
            else
            {
                items = selectedCtrl.GetSelectedRows().Where(x=>x.ToApplyTotal > 0).ToList();
            }
            if (items.All(x => string.IsNullOrWhiteSpace(x.PoNumber)))
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                args.Caption = "请输入PO编号";
                args.Prompt = "PO编号";
                args.DefaultButtonIndex = 0;
                var listofPOs = _managementService.GetAllPoNumbers().Select(x => x.PoNumber).ToArray();
                var editor = new ComboBoxEdit();
                editor.Properties.Items.AddRange(listofPOs);
                args.Editor = editor;
                PONumber = XtraInputBox.Show(args)?.ToString();
                if (string.IsNullOrWhiteSpace(PONumber))
                {
                    MessageBox.Show("必须填写PO编号");
                    return;
                }           
            }
            else PONumber = items.FirstOrDefault().PoNumber;
            //输入自定义的applicationnumber
            XtraInputBoxArgs args1 = new XtraInputBoxArgs();
            args1.Caption = "输入采购申请编号";
            args1.Prompt = "采购申请编号";
            args1.DefaultButtonIndex = 0;
            temp_applicationNumber = XtraInputBox.Show(args1)?.ToString();
            SplashScreenManager.ShowDefaultWaitForm();
            _purchaseService.CreateApplication(items, PONumber, temp_applicationNumber);
            SplashScreenManager.CloseDefaultWaitForm();
            selectedCtrl.RefreshData(true);

        }

        //审核
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            PurchaseApplicationCtrl selectedCtrl;
            var tabName = (ApplicationCategoriesEnum)Enum.Parse(typeof(ApplicationCategoriesEnum), this.xtraTabControl2.SelectedTabPage.Text);
            switch (tabName)
            {
                case ApplicationCategoriesEnum.采购申请汇总:
                    selectedCtrl= (PurchaseApplicationCtrl)this.xtraTabControl2.SelectedTabPage.Controls[0];
                    break;
                case ApplicationCategoriesEnum.退货申请汇总:
                    selectedCtrl = (RefundApplicationCtrl)this.xtraTabControl2.SelectedTabPage.Controls[0];
                    break;
                default: //补给申请
                    selectedCtrl = (PurchaseApplicationCtrl)this.xtraTabControl2.SelectedTabPage.Controls[0];
                    break;
            } 
            var items = selectedCtrl.GetSelectedRows();
            
            _purchaseService.ConfirmAllApplications(items);
            
            barButtonItem15.Enabled = false;
            SplashScreenManager.CloseDefaultWaitForm();
            switch (tabName)
            {
                case ApplicationCategoriesEnum.采购申请汇总:
                    _purchaseApplicationCtrl.RefreshData(true);
                    break;
                case ApplicationCategoriesEnum.退货申请汇总:
                    _refundApplicationCtrl.RefreshData(true);
                    break;
                default:
                    return;
            }

        }

        //生成采购单
        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ctrl = (PurchaseApplicationCtrl)this.xtraTabControl2.SelectedTabPage.Controls[0];

            //输入自定义的采购单号
            XtraInputBoxArgs args = new XtraInputBoxArgs();
            args.Caption = "输入采购单编号";
            args.Prompt = "采购单编号";
            args.DefaultButtonIndex = 0;
            var temp_purchaseNumber = XtraInputBox.Show(args)?.ToString();

            SplashScreenManager.ShowDefaultWaitForm();
            var items = ctrl.GetSelectedRows();
            var listOfSuppliers = items.GroupBy(x => x.SupplierName).Select(s => s.Key).ToList();
            foreach (var supplier in listOfSuppliers)
            {
                var models = items.Where(x => x.SupplierName == supplier).ToList();
                _purchaseService.CreatePurchase(models, temp_purchaseNumber);
            }
            SplashScreenManager.CloseDefaultWaitForm();
            _purchaseApplicationCtrl.RefreshData(true);
        }

        //生成退货单
        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctrl = (RefundApplicationCtrl)this.xtraTabControl2.SelectedTabPage.Controls[0];

            //输入自定义的退货单
            XtraInputBoxArgs args = new XtraInputBoxArgs();
            args.Caption = "输入退货单编号";
            args.Prompt = "退货单编号";
            args.DefaultButtonIndex = 0;
            var temp_purchaseNumber = XtraInputBox.Show(args)?.ToString();

            SplashScreenManager.ShowDefaultWaitForm();
            var items = ctrl.GetSelectedRows();
            var listOfSuppliers = items.GroupBy(x => x.SupplierName).Select(s => s.Key).ToList();
            foreach (var supplier in listOfSuppliers)
            {
                var models = items.Where(x => x.SupplierName == supplier).ToList();
                _purchaseService.CreatePurchase(models, temp_purchaseNumber);
            }
            SplashScreenManager.CloseDefaultWaitForm();
            _refundApplicationCtrl.RefreshData(true);
        }

        //生成Excel 
        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export excel file";
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                var ctrl = (PurchaseApplicationCtrl)this.xtraTabControl2.SelectedTabPage.Controls[0];
                SplashScreenManager.ShowDefaultWaitForm();
                var items = ctrl.GetSelectedRows();
                _purchaseService.ExportPurchaseApplicatoin(items, saveFileDialog.FileName);
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        #endregion

        #region 采购

        //到货操作
        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctrl = (PurchaseCtrl)this.xtraTabControl3.SelectedTabPage.Controls[0];
            SplashScreenManager.ShowDefaultWaitForm();
            var items = ctrl.GetSelectedRows();
            _purchaseService.FillAllPurchaseTotal(items);
            SplashScreenManager.CloseDefaultWaitForm();
            ctrl.RefreshData(true);
        }

        //入库操作
        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctrl = (PurchaseCtrl)this.xtraTabControl3.SelectedTabPage.Controls[0];

            //输入自定义的入库编号
            XtraInputBoxArgs args1 = new XtraInputBoxArgs();
            args1.Caption = "输入入库编号";
            args1.Prompt = "入库编号";
            args1.DefaultButtonIndex = 0;
            var temp_instockNumber = XtraInputBox.Show(args1)?.ToString();

            SplashScreenManager.ShowDefaultWaitForm();
            var items = ctrl.GetSelectedRows();
            _stockService.CreateInStock(items, temp_instockNumber);
            SplashScreenManager.CloseDefaultWaitForm();
            ctrl.RefreshData(true);
        }

        //生成采购Excel 
        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export excel file";
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                var ctrl = (PurchaseCtrl)this.xtraTabControl3.SelectedTabPage.Controls[0];
                SplashScreenManager.ShowDefaultWaitForm();
                var items = ctrl.GetSelectedRows();

                if (MessageBox.Show("合并采购单", "是否需要合并采购单", MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
                            DialogResult.No)
                {
                    _purchaseService.ExportPurchase(items, saveFileDialog.FileName, true);
                }
                else
                {
                    _purchaseService.ExportPurchase(items, saveFileDialog.FileName, false);
                }
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }


        #endregion

        #region 入库
        //入库单汇总
        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            _inStockCtrl.RefreshData();
            AddControlToPage(_inStockCtrl, TabCategory.入库, "入库单汇总");
        }

        //生成入库Excel
        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export excel file";
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                var ctrl = (InStockCtrl)this.xtraTabControl4.SelectedTabPage.Controls[0];
                SplashScreenManager.ShowDefaultWaitForm();
                var items = ctrl.GetSelectedHeaderRows();
                _stockService.ExportInStock(items, saveFileDialog.FileName);
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        #endregion

        #region 出库
        //出库单汇总
        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            _inDeliveryCtrl.RefreshData();
            AddControlToPage(_inDeliveryCtrl, TabCategory.出库, "出库单汇总");
        }

        //出库操作

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedCtrl = (RequestHeadersCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
            var items = selectedCtrl.GetSelectedRows();
            if(items.Any(x=>x.ToOutStockTotal > x.TotalInStorage) || items.Any(x=>x.ToOutStockTotal > x.Total - x.OutStockTotal))
            {MessageBox.Show("出库数量超出库存数");
                return;
            }
            else
            {
                //输入自定义的出库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入出库编号";
                args1.Prompt = "出库编号";
                args1.DefaultButtonIndex = 0;
                var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateOutStock(items, temp_outstockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }

        //生成出库Excel
        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export excel file";
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                var ctrl = (InDeliveryCtrl)this.xtraTabControl5.SelectedTabPage.Controls[0];
                SplashScreenManager.ShowDefaultWaitForm();
                var items = ctrl.GetSelectedHeaderRows();
                _stockService.ExportOutStock(items, saveFileDialog.FileName);
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        #endregion

        #region 借出
        //借出操作
        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工具借出:
                    selectedCtrl = (LoanRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (selectedCtrl.IsValidated(items))
            {
                //输入自定义的出库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入出库编号";
                args1.Prompt = "出库编号";
                args1.DefaultButtonIndex = 0;
                var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateOutStock(items, temp_outstockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }

        //归还操作
        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工具借出:
                    selectedCtrl = (LoanRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (items.Count > 0)
            {
                if (items.Any(x => x.ToInStockTotal > x.OutStockTotal))
                {
                    MessageBox.Show("入库数量不能大于出库数量");
                }
                else
                {
                    //输入自定义的入库编号
                    XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                    args1.Caption = "输入入库编号";
                    args1.Prompt = "入库编号";
                    args1.DefaultButtonIndex = 0;
                    var temp_instockNumber = XtraInputBox.Show(args1)?.ToString();

                    SplashScreenManager.ShowDefaultWaitForm();
                    _stockService.CreateInStock(items, temp_instockNumber);
                    SplashScreenManager.CloseDefaultWaitForm();
                    selectedCtrl.RefreshData(true);
                }
            }
        }

        #endregion

        #region 补给

        //补给
        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工程车补给:
                    selectedCtrl = (CarRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.员工补给:
                    selectedCtrl = (EmployeeRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if(items.Any(x=>x.ToOutStockTotal <= 0))
            {
                MessageBox.Show("补给数量必须大于0");
                return;
            }
            else
            {
                //输入自定义的出库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入出库编号";
                args1.Prompt = "出库编号";
                args1.DefaultButtonIndex = 0;
                var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateOutStock(items, temp_outstockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }

        #endregion

        #region 维修

        //维修入库
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工程车维修:
                    selectedCtrl = (CarFixRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();

            //输入自定义的入库编号
            XtraInputBoxArgs args1 = new XtraInputBoxArgs();
            args1.Caption = "输入入库编号";
            args1.Prompt = "入库编号";
            args1.DefaultButtonIndex = 0;
            var temp_instockNumber = XtraInputBox.Show(args1)?.ToString();

            SplashScreenManager.ShowDefaultWaitForm();
            _stockService.CreateInStock(items, temp_instockNumber);
            SplashScreenManager.CloseDefaultWaitForm();
            selectedCtrl.RefreshData(true);
        }
        //维修出库
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工程车维修:
                    selectedCtrl = (CarFixRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (selectedCtrl.IsValidated(items))
            {
                //输入自定义的出库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入出库编号";
                args1.Prompt = "出库编号";
                args1.DefaultButtonIndex = 0;
                var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateOutStock(items, temp_outstockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }
        //维修开始
        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工具维修:
                    selectedCtrl = (ToolRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.工程车维修:
                    selectedCtrl = (CarFixRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (selectedCtrl.IsValidated(items))
            {
                //输入自定义的出库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入出库编号";
                args1.Prompt = "出库编号";
                args1.DefaultButtonIndex = 0;
                var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateOutStock(items, temp_outstockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }
        //维修结束
        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工具维修:
                    selectedCtrl = (ToolRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.工程车维修:
                    selectedCtrl = (CarFixRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (selectedCtrl.IsValidated(items))
            {
                //输入自定义的入库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入入库编号";
                args1.Prompt = "入库编号";
                args1.DefaultButtonIndex = 0;
                var temp_instockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateInStock(items, temp_instockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }

        //报废
        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.工具维修:
                    selectedCtrl = (ToolRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                case RequestCategoriesEnum.工程车维修:
                    selectedCtrl = (CarFixRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (selectedCtrl.IsValidated(items))
            {
                if (items.Any(x => x.ToDestoryTotal > (x.Total - x.DestoriedTotal)))
                {
                    MessageBox.Show("已达最大可报废数量");
                    return;
                }
                if (items.Any(x => x.ToDestoryTotal == 0))
                {
                    MessageBox.Show("可报废数量不能为0");
                    return;
                }
                else
                {
                    //输入自定义的出库编号
                    XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                    args1.Caption = "输入出库编号";
                    args1.Prompt = "出库编号";
                    args1.DefaultButtonIndex = 0;
                    var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

                    SplashScreenManager.ShowDefaultWaitForm();
                    foreach (var item in items)
                    {
                        item.ToDestroy = true;
                        item.ToOutStockTotal = item.ToDestoryTotal;
                    }
                    _stockService.CreateOutStock(items,temp_outstockNumber);
                    SplashScreenManager.CloseDefaultWaitForm();
                    selectedCtrl.RefreshData(true);
                }
            }
        }

        #endregion

        #region 物品退回
        //物品退回
        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            RequestHeadersCtrl selectedCtrl;
            var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
            switch (tabName)
            {
                case RequestCategoriesEnum.物品退回:
                    selectedCtrl = (ReturnRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                    break;
                default:
                    return;
            }
            var items = selectedCtrl.GetSelectedRows();
            if (selectedCtrl.IsValidated(items))
            {
                //输入自定义的入库编号
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入入库编号";
                args1.Prompt = "入库编号";
                args1.DefaultButtonIndex = 0;
                var temp_instockNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _stockService.CreateInStock(items, temp_instockNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }

        #endregion

        #region 退货申请
        //退货申请
        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
                RequestHeadersCtrl selectedCtrl;
                var tabName = (RequestCategoriesEnum)Enum.Parse(typeof(RequestCategoriesEnum), this.xtraTabControl1.SelectedTabPage.Text);
                switch (tabName)
                {
                    case RequestCategoriesEnum.采购退货:
                        selectedCtrl = (RefundRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
                        break;
                    default:
                        return;
                }
                
                var items = selectedCtrl.GetSelectedRows();
                if (items.Any(x => x.Total != x.ToApplyTotal))
                {
                    MessageBox.Show(@"退货申请数量必须等于需求数量");
                    return;
                }

            if (selectedCtrl.IsValidated(items))
            {
                //输入自定义的applicationnumber
                XtraInputBoxArgs args1 = new XtraInputBoxArgs();
                args1.Caption = "输入退货申请编号";
                args1.Prompt = "退货申请编号";
                args1.DefaultButtonIndex = 0;
                var temp_applicationNumber = XtraInputBox.Show(args1)?.ToString();

                SplashScreenManager.ShowDefaultWaitForm();
                _purchaseService.CreateApplication(items, string.Empty, temp_applicationNumber);
                SplashScreenManager.CloseDefaultWaitForm();
                selectedCtrl.RefreshData(true);
            }
        }
        #endregion

        #region 退货
        //退货
        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedCtrl = (RefundRequestCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
            var items = selectedCtrl.GetSelectedRows();
            if (items.Any(x => x.ToOutStockTotal > x.TotalInStorage) || items.Any(x => x.ToOutStockTotal > x.Total))
            {
                MessageBox.Show("出库数量超出库存数");
                return;
            }
            //输入自定义的出库编号
            XtraInputBoxArgs args1 = new XtraInputBoxArgs();
            args1.Caption = "输入出库编号";
            args1.Prompt = "出库编号";
            args1.DefaultButtonIndex = 0;
            var temp_outstockNumber = XtraInputBox.Show(args1)?.ToString();

            SplashScreenManager.ShowDefaultWaitForm();
            _stockService.CreateOutStock(items, temp_outstockNumber);
            SplashScreenManager.CloseDefaultWaitForm();
            selectedCtrl.RefreshData(true);
        }

        #endregion

        #region 管理
        //合同编号
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _contractCtrl.RefreshData();
            AddControlToPage(_contractCtrl, TabCategory.管理, "合同编号管理");
        }

        //上传仓库资料汇总表
        private async void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var filename = CustomizeHelper.LoadFileName();
            int result = 0;
            if (filename == null)
            {
                return;
            }
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                string returnMessage = string.Empty;
                await Task.Run(() =>
                {
                    result = _managementService.Upload(filename, out returnMessage);
                });
                switch (result)
                {
                    case 1:
                        MessageBox.Show("物品资料汇总上传成功!");
                        break;
                    case 2:
                        MessageBox.Show("物品资料汇总已经存在 :" + returnMessage);
                        break;
                    default:
                        break;
                }
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        //上传库位表
        private async void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            var filename = CustomizeHelper.LoadFileName();
            int result = 0;
            if (filename == null)
            {
                return;
            }
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                string returnMessage = string.Empty;
                await Task.Run(() =>
                {
                    result = _managementService.UploadRelationship(filename);
                });
                switch (result)
                {
                    case 1:
                        MessageBox.Show("库位上传成功!");
                        break;
                    case -2:
                        MessageBox.Show("库位已经存在");
                        break;
                    case -3:
                        MessageBox.Show("请先上传物品资料汇总表");
                        break;
                    default:
                        break;
                }
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        //上传盘点表
        private async void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var filename = CustomizeHelper.LoadFileName();
            int result = 0;
            if (filename == null)
            {
                return;
            }
            else
            {
                SplashScreenManager.ShowDefaultWaitForm();
                string returnMessage = string.Empty;
                await Task.Run(() =>
                {
                    result = _managementService.UpdateTotalNumberBySummarySheet(filename,out returnMessage);
                });
                switch (result)
                {
                    case 1:
                        MessageBox.Show("更新成功!");
                        break;
                    case -2:
                        MessageBox.Show("更新失败");
                        break;
                    default:
                        break;
                }
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        //显示仓库资料汇总表
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            _itemManagementCtrl.RefreshData();
            AddControlToPage(_itemManagementCtrl, TabCategory.管理, "仓库物品资料汇总");
            SplashScreenManager.CloseDefaultWaitForm();
        }

        //物品盘点
        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export excel file";
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                //var ctrl = (InStockCtrl)this.xtraTabControl4.SelectedTabPage.Controls[0];
                SplashScreenManager.ShowDefaultWaitForm();
                //var items = ctrl.GetSelectedHeaderRows();
                _managementService.ExportItemExcel(saveFileDialog.FileName, ReportNameEnum.物品盘点,DateTime.Now);
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        //捡货
        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ctrl = (RequestHeadersCtrl)this.xtraTabControl1.SelectedTabPage.Controls[0];
            var items = ctrl.GetSelectedHeaderRows();
            if (items.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Export excel file";
                saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != "")
                {
                    //var ctrl = (InStockCtrl)this.xtraTabControl4.SelectedTabPage.Controls[0];
                    SplashScreenManager.ShowDefaultWaitForm();
                    //var items = ctrl.GetSelectedHeaderRows();
                    _requestService.ExportItemExcel(saveFileDialog.FileName, ReportNameEnum.捡货, items);
                    SplashScreenManager.CloseDefaultWaitForm();
                }
            }
        }

        #region 供应商
        //供应商
        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            _supplierManagementCtrl.RefreshData();
            AddControlToPage(_supplierManagementCtrl, TabCategory.管理, "供应商管理");           
        }

        //新建供应商
        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraInputBoxArgs args = new XtraInputBoxArgs();
            args.Caption = "请输入供应商名字";
            args.Prompt = "供应商名字";
            args.DefaultButtonIndex = 0;
            var editor = new TextEdit();
            args.Editor = editor;
            var result = XtraInputBox.Show(args);

            if (result != null)
            {
                SplashScreenManager.ShowDefaultWaitForm();
                _supplierManagementCtrl.CreateNewSuppiler(result.ToString());
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        #endregion

        #endregion

        #region General

        //选择最上面的Tab
        private void ribbonControl_SelectedPageChanged(object sender, EventArgs e)
        {
            RibbonControl ribbon = sender as RibbonControl;
            var tab = (TabCategory)Enum.Parse(typeof(TabCategory), ribbon.SelectedPage.Text);

            this.xtraTabControl1.Visible = false;
            this.xtraTabControl2.Visible = false;
            this.xtraTabControl3.Visible = false;
            this.xtraTabControl4.Visible = false;
            this.xtraTabControl5.Visible = false;
            this.xtraTabControl6.Visible = false;
            switch (tab)
            {
                case TabCategory.需求:
                    this.xtraTabControl1.Visible = true;
                    //_materialRequestCtrl.RefreshData(true);
                    //AddControlToPage(_materialRequestCtrl, TabCategory.需求, RequestCategoriesEnum.材料需求.ToString());
                    break;
                case TabCategory.采购申请:
                    this.xtraTabControl2.Visible = true;
                    //_purchaseApplicationCtrl.RefreshData(true);
                    //AddControlToPage(_purchaseApplicationCtrl, TabCategory.采购申请, ApplicationCategoriesEnum.材料申请.ToString());
                    break;
                case TabCategory.采购单:
                    this.xtraTabControl3.Visible = true;
                   // _purchaseCtrl.RefreshData(true);
                    //AddControlToPage(_purchaseCtrl, TabCategory.采购单, PurchaseCategoriesEnum.采购单.ToString());
                    break;
                case TabCategory.入库:
                    this.xtraTabControl4.Visible = true;
                    //_inStockCtrl.RefreshData();
                    //AddControlToPage(_inStockCtrl, TabCategory.入库, "入库单汇总");
                    break;
                case TabCategory.出库:
                    this.xtraTabControl5.Visible = true;
                    //_inDeliveryCtrl.RefreshData();
                    //AddControlToPage(_inDeliveryCtrl, TabCategory.出库, "出库单汇总");
                    break;
                case TabCategory.管理:
                    this.xtraTabControl6.Visible = true;
                    break;
                default:
                    break;
            }


        }

        //加入新的Tab
        public void AddControlToPage(XtraUserControl ctrl, TabCategory tabCategory, string tabDisplayName)
        {
            var currentXtraTabControl = this.xtraTabControl1;
            switch (tabCategory)
            {
                case TabCategory.需求:
                    currentXtraTabControl = this.xtraTabControl1;
                    barButtonItem43.Enabled = true;
                    break;
                case TabCategory.采购申请:
                    currentXtraTabControl = this.xtraTabControl2;
                    break;
                case TabCategory.采购单:
                    currentXtraTabControl = this.xtraTabControl3;
                    break;
                case TabCategory.入库:
                    currentXtraTabControl = this.xtraTabControl4;
                    break;
                case TabCategory.出库:
                    currentXtraTabControl = this.xtraTabControl5;
                    break;
                case TabCategory.管理:
                    currentXtraTabControl = this.xtraTabControl6;
                    break;
                default:
                    break;
            }

            string tabName = tabDisplayName + "_TabPage";
            foreach (var item in currentXtraTabControl.TabPages.ToList())
            {
                if (item.Name == tabName)
                {
                    //item.Controls.RemoveAt(0);
                    //item.Controls.Add(ctrl);
                    currentXtraTabControl.SelectedTabPage = item;
                    return;
                }
            }
            XtraTabPage page = new XtraTabPage()
            {
                Name = tabName,
                Text = tabDisplayName,
            };
            page.Controls.Add(ctrl);
            currentXtraTabControl.ClosePageButtonShowMode = ClosePageButtonShowMode.InActiveTabPageHeaderAndOnMouseHover;
            currentXtraTabControl.TabPages.Add(page);
            currentXtraTabControl.SelectedTabPage = page;
        }

        //Close Tab窗口
        private void xtraTabControl_CloseButtonClick(object sender, EventArgs e)
        {
            XtraTabControl _current;
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            var control = arg.Page.TabControl as XtraTabControl;
            var tab = (TabCategory)Enum.Parse(typeof(TabCategory), control.Text);
            switch (tab)
            {
                case TabCategory.需求:
                    _current = this.xtraTabControl1;
                    break;
                case TabCategory.采购申请:
                    _current = this.xtraTabControl2;
                    break;
                case TabCategory.采购单:
                    _current = this.xtraTabControl3;
                    break;
                case TabCategory.入库:
                    _current = this.xtraTabControl4;
                    break;
                case TabCategory.出库:
                    _current = this.xtraTabControl5;
                    break;
                case TabCategory.管理:
                    _current = this.xtraTabControl6;
                    break;
                default:
                    _current = this.xtraTabControl1;
                    break;
            }
            _current.TabPages.Remove((arg.Page as XtraTabPage));
        }

        public enum TabCategory {
            需求,
            采购申请,
            采购单,
            出库,
            入库,
            管理
        }


        #endregion

    }
}
