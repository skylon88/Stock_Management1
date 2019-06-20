using DevExpress.XtraGrid.Views.Base;
using NewServices.Models.采购部分;

namespace App.Views.采购部分
{
    partial class PurchaseCtrl
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.purchaseHeaderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPurchaseNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPONumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationDept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatePerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditDepart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriority = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurchaseType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompletePercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurchaseCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseHeaderViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.purchaseHeaderViewModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
           // this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1638, 748);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // purchaseHeaderViewModelBindingSource
            // 
            this.purchaseHeaderViewModelBindingSource.DataSource = typeof(PurchaseHeaderViewModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPurchaseNumber,
            this.colApplicationNumber,
            this.colPONumber,
            this.colSupplierName,
            this.colApplicationDept,
            this.colCreatePerson,
            this.colAuditor,
            this.colAuditDepart,
            this.colPriority,
            this.colPurchaseType,
            this.colTotalPrice,
            this.colCompletePercentage,
            this.colPurchaseCategory,
            this.colDeliveryCategory,
            this.colDeliveryDate,
            this.colCreateDate,
            this.colUpdateDate,
            this.col});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.MasterRowExpanding += new DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventHandler(this.gridView1_MasterRowExpanding);
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            this.gridView1.MasterRowGetRelationDisplayCaption += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gridView1_MasterRowGetRelationDisplayCaption);
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.RowCellStyle += gridView_RowCellStyle;
            // 
            // colPurchaseType
            // 
            this.colPurchaseType.FieldName = "PurchaseType";
            this.colPurchaseType.Name = "colPurchaseType";
            this.colPurchaseType.Visible = true;
            this.colPurchaseType.VisibleIndex = 1;
            // 
            // colPurchaseNumber
            // 
            this.colPurchaseNumber.FieldName = "PurchaseNumber";
            this.colPurchaseNumber.Name = "colPurchaseNumber";
            this.colPurchaseNumber.Visible = true;
            this.colPurchaseNumber.VisibleIndex = 2;
            // 
            // colApplicationNumber
            // 
            this.colApplicationNumber.FieldName = "ApplicationNumber";
            this.colApplicationNumber.Name = "colApplicationNumber";
            this.colApplicationNumber.Visible = true;
            this.colApplicationNumber.VisibleIndex = 3;
            // 
            // colPONumber
            // 
            this.colPONumber.FieldName = "PoNumber";
            this.colPONumber.Name = "colPoNumber";
            this.colPONumber.Visible = true;
            this.colPONumber.VisibleIndex = 4;
            // 
            // colSupplierName
            // 
            this.colSupplierName.FieldName = "SupplierName";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.Visible = true;
            this.colSupplierName.VisibleIndex = 5;
            // 
            // colApplicationDept
            // 
            this.colApplicationDept.FieldName = "ApplicationDept";
            this.colApplicationDept.Name = "colApplicationDept";
            this.colApplicationDept.Visible = false;
            this.colApplicationDept.VisibleIndex = 6;
            // 
            // colCreatePerson
            // 
            this.colCreatePerson.FieldName = "CreatePerson";
            this.colCreatePerson.Name = "colCreatePerson";
            this.colCreatePerson.Visible = true;
            this.colCreatePerson.VisibleIndex = 7;
            // 
            // colAuditor
            // 
            this.colAuditor.FieldName = "Auditor";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.Visible = true;
            this.colAuditor.VisibleIndex = 8;
            // 
            // colAuditDepart
            // 
            this.colAuditDepart.FieldName = "AuditDepart";
            this.colAuditDepart.Name = "colAuditDepart";
            this.colAuditDepart.Visible = true;
            this.colAuditDepart.VisibleIndex = 9;
            // 
            // colPriority
            // 
            this.colPriority.FieldName = "Priority";
            this.colPriority.Name = "colPriority";
            this.colPriority.Visible = true;
            this.colPriority.VisibleIndex = 10;
            
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.DisplayFormat.FormatString = "c2";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.VisibleIndex = 11;
            // 
            // CompletePercentage
            // 
            this.colCompletePercentage.FieldName = "CompletePercentage";
            this.colCompletePercentage.Name = "colCompletePercentage";
            this.colCompletePercentage.Visible = true;
            this.colCompletePercentage.VisibleIndex = 12;
            // 
            // colPurchaseCategory
            // 
            this.colPurchaseCategory.FieldName = "PurchaseCategory";
            this.colPurchaseCategory.Name = "PurchaseCategory";
            this.colPurchaseCategory.Visible = true;
            this.colPurchaseCategory.VisibleIndex = 13;
            // 
            // colDeliveryCategory
            // 
            this.colDeliveryCategory.FieldName = "DeliveryCategory";
            this.colDeliveryCategory.Name = "DeliveryCategory";
            this.colDeliveryCategory.Visible = true;
            this.colDeliveryCategory.VisibleIndex = 14;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.FieldName = "DeliveryDate";
            this.colDeliveryDate.Name = "DeliveryDate";
            this.colDeliveryDate.Visible = true;
            this.colDeliveryDate.VisibleIndex = 15;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "CreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 16;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "UpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 17;
            // 
            // col
            // 
            this.col.Caption = "到货数量";
            this.col.Name = "col";

            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "重新申请", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);

            // 
            // PurchaseCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "PurchaseCtrl";
            this.Size = new System.Drawing.Size(1638, 748);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseHeaderViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchaseNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPONumber;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationDept;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatePerson;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditDepart;
        private DevExpress.XtraGrid.Columns.GridColumn colPriority;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchaseType;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colCompletePercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchaseCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn col;
        private System.Windows.Forms.BindingSource purchaseHeaderViewModelBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}
