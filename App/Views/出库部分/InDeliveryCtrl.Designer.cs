using NewServices.Models.出库部分;

namespace App.Views.出库部分
{
    partial class InDeliveryCtrl
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
            ///this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.outStockHeaderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOutStockNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationDept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatePerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditDepart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPickupPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPickupGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutStockCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.outStockViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outStockHeaderViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outStockViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.outStockHeaderViewModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            //this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(524, 330);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // outStockHeaderViewModelBindingSource
            // 
            this.outStockHeaderViewModelBindingSource.DataSource = typeof(OutStockHeaderViewModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOutStockNumber,
            this.colRequestNumber,
            this.colContractNumber,
            this.colAddress,
            this.colApplicationDept,
            this.colCreatePerson,
            this.colAuditor,
            this.colAuditDepart,
            this.colPickupPerson,
            this.colPickupGroup,
            this.colOutStockCategory,
            this.colCreateDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.MasterRowExpanding += new DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventHandler(this.gridView1_MasterRowExpanding);
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            this.gridView1.MasterRowGetRelationDisplayCaption += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gridView1_MasterRowGetRelationDisplayCaption);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.RowCellStyle += gridView_RowCellStyle;
            // 
            // colOutStockNumber
            // 
            this.colOutStockNumber.FieldName = "OutStockNumber";
            this.colOutStockNumber.Name = "colOutStockNumber";
            this.colOutStockNumber.Visible = true;
            this.colOutStockNumber.VisibleIndex = 0;
            // 
            // colOutStockCategory
            // 
            this.colOutStockCategory.FieldName = "OutStockCategory";
            this.colOutStockCategory.Name = "colOutStockCategory";
            this.colOutStockCategory.Visible = true;
            this.colOutStockCategory.VisibleIndex = 1;
            // 
            // colRequestNumber
            // 
            this.colRequestNumber.FieldName = "RequestNumber";
            this.colRequestNumber.Name = "colRequestNumber";
            this.colRequestNumber.Visible = true;
            this.colRequestNumber.VisibleIndex = 2;
            // 
            // colContractNumber
            // 
            this.colContractNumber.FieldName = "ContractNumber";
            this.colContractNumber.Name = "colContractNumber";
            this.colContractNumber.Visible = true;
            this.colContractNumber.VisibleIndex = 3;
            // 
            // colAddress
            // 
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 4;
            // 
            // colApplicationDept
            // 
            this.colApplicationDept.FieldName = "ApplicationDept";
            this.colApplicationDept.Name = "colApplicationDept";
            this.colApplicationDept.Visible = true;
            this.colApplicationDept.VisibleIndex = 5;
            // 
            // colCreatePerson
            // 
            this.colCreatePerson.FieldName = "CreatePerson";
            this.colCreatePerson.Name = "colCreatePerson";
            this.colCreatePerson.Visible = true;
            this.colCreatePerson.VisibleIndex = 6;
            // 
            // colAuditor
            // 
            this.colAuditor.FieldName = "Auditor";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.Visible = true;
            this.colAuditor.VisibleIndex = 7;
            // 
            // colAuditDepart
            // 
            this.colAuditDepart.FieldName = "AuditDepart";
            this.colAuditDepart.Name = "colAuditDepart";
            this.colAuditDepart.Visible = true;
            this.colAuditDepart.VisibleIndex = 8;
            // 
            // colPickupPerson
            // 
            this.colPickupPerson.FieldName = "PickupPerson";
            this.colPickupPerson.Name = "colPickupPerson";
            this.colPickupPerson.Visible = true;
            this.colPickupPerson.VisibleIndex = 9;
            // 
            // colPickupGroup
            // 
            this.colPickupGroup.FieldName = "PickupGroup";
            this.colPickupGroup.Name = "colPickupGroup";
            this.colPickupGroup.Visible = true;
            this.colPickupGroup.VisibleIndex = 10;
            

            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 10;

            // 
            // outStockViewModelBindingSource
            // 
            this.outStockViewModelBindingSource.DataSource = typeof(OutStockViewModel);
            // 
            // InDeliveryCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "InDeliveryCtrl";
            this.Size = new System.Drawing.Size(524, 330);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outStockHeaderViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outStockViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource outStockHeaderViewModelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colOutStockNumber; 
        private DevExpress.XtraGrid.Columns.GridColumn colRequestNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationDept;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatePerson;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditDepart;
        private DevExpress.XtraGrid.Columns.GridColumn colPickupPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colPickupGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colOutStockCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private System.Windows.Forms.BindingSource outStockViewModelBindingSource;
    }
}
