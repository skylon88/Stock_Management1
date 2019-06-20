using NewServices.Models.入库部分;

namespace App.Views.入库部分
{
    partial class InStockCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.instockHeaderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInStockNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurchaseNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPoNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationDept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatePerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditDepart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInStockCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instockHeaderViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.instockHeaderViewModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            //this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(958, 468);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // instockHeaderViewModelBindingSource
            // 
            this.instockHeaderViewModelBindingSource.DataSource = typeof(InStockHeaderViewModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInStockNumber,
            this.colPurchaseNumber,
            this.colRequestNumber,
            this.colPoNumber,
            this.colContractNumber,
            this.colSupplierName,
            this.colApplicationDept,
            this.colCreatePerson,
            this.colAuditor,
            this.colAuditDepart,
            this.colInStockCategory,
            this.colCreateDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.MasterRowExpanding += new DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventHandler(this.gridView1_MasterRowExpanding);
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            this.gridView1.MasterRowGetRelationDisplayCaption += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gridView1_MasterRowGetRelationDisplayCaption);
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.RowCellStyle += gridView_RowCellStyle;
            // 
            // colInStockNumber
            // 
            this.colInStockNumber.FieldName = "InStockNumber";
            this.colInStockNumber.Name = "colInStockNumber";
            this.colInStockNumber.Visible = true;
            this.colInStockNumber.VisibleIndex = 0;
            // 
            // colInStockCategory
            // 
            this.colInStockCategory.FieldName = "InStockCategory";
            this.colInStockCategory.Name = "colInStockCategory";
            this.colInStockCategory.Visible = true;
            this.colInStockCategory.VisibleIndex = 1;
            // 
            // colRequestNumber
            // 
            this.colRequestNumber.FieldName = "RequestNumber";
            this.colRequestNumber.Name = "colRequestNumber";
            this.colRequestNumber.Visible = true;
            this.colRequestNumber.VisibleIndex = 2;
            // 
            // colPurchaseNumber
            // 
            this.colPurchaseNumber.FieldName = "PurchaseNumber";
            this.colPurchaseNumber.Name = "colPurchaseNumber";
            this.colPurchaseNumber.Visible = true;
            this.colPurchaseNumber.VisibleIndex = 3;
            // 
            // colPoNumber
            // 
            this.colPoNumber.FieldName = "PoNumber";
            this.colPoNumber.Name = "colPoNumber";
            this.colPoNumber.Visible = true;
            this.colPoNumber.VisibleIndex = 4;
            // 
            // colContractNumber
            // 
            this.colContractNumber.FieldName = "ContractNumber";
            this.colContractNumber.Name = "colContractNumber";
            this.colContractNumber.Visible = true;
            this.colContractNumber.VisibleIndex = 5;
            // 
            // colSupplierName
            // 
            this.colSupplierName.FieldName = "SupplierName";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.Visible = true;
            this.colSupplierName.VisibleIndex = 6;
            // 
            // colApplicationDept
            // 
            this.colApplicationDept.FieldName = "ApplicationDept";
            this.colApplicationDept.Name = "colApplicationDept";
            this.colApplicationDept.Visible = true;
            this.colApplicationDept.VisibleIndex = 7;
            // 
            // colCreatePerson
            // 
            this.colCreatePerson.FieldName = "CreatePerson";
            this.colCreatePerson.Name = "colCreatePerson";
            this.colCreatePerson.Visible = true;
            this.colCreatePerson.VisibleIndex = 8;
            // 
            // colAuditor
            // 
            this.colAuditor.FieldName = "Auditor";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.Visible = true;
            this.colAuditor.VisibleIndex = 9;
            // 
            // colAuditDepart
            // 
            this.colAuditDepart.FieldName = "AuditDepart";
            this.colAuditDepart.Name = "colAuditDepart";
            this.colAuditDepart.Visible = true;
            this.colAuditDepart.VisibleIndex = 10;
            
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 11;
            // 
            // InStockCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "InStockCtrl";
            this.Size = new System.Drawing.Size(958, 468);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instockHeaderViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.BindingSource instockHeaderViewModelBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colInStockNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchaseNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPoNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationDept;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatePerson;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditDepart;
        private DevExpress.XtraGrid.Columns.GridColumn colInStockCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
    }
}
