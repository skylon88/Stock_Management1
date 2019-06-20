using System;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewServices.Models.采购部分;

namespace App.Views.采购部分
{
    partial class PurchaseApplicationCtrl
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.purchaseApplicationHeaderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colApplicationNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestHeaderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPoNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatePerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFollowupPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditDepart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalApplied = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalConfirmed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompletePercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCopy = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseApplicationHeaderViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.purchaseApplicationHeaderViewModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1585, 455);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // purchaseApplicationHeaderViewModelBindingSource
            // 
            this.purchaseApplicationHeaderViewModelBindingSource.DataSource = typeof(NewServices.Models.采购部分.PurchaseApplicationHeaderViewModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colApplicationNumber,
            this.colRequestHeaderNumber,
            this.colPoNumber,
            this.colApplicationPerson,
            this.colCreatePerson,
            this.colFollowupPerson,
            this.colAuditor,
            this.colAuditDepart,
            this.colTotalApplied,
            this.colTotalConfirmed,
            this.colAuditStatus,
            this.colStatus,
            this.colCompletePercentage,
            this.colCreateDate,
            this.colUpdateDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.MasterRowExpanding += new DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventHandler(this.gridView1_MasterRowExpanding);
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            this.gridView1.MasterRowGetRelationDisplayCaption += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gridView1_MasterRowGetRelationDisplayCaption);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.RowCellStyle += gridView_RowCellStyle;
            // 
            // colApplicationNumber
            // 
            this.colApplicationNumber.FieldName = "ApplicationNumber";
            this.colApplicationNumber.Name = "colApplicationNumber";
            this.colApplicationNumber.Visible = true;
            this.colApplicationNumber.VisibleIndex = 0;
            // 
            // colRequestHeaderNumber
            // 
            this.colRequestHeaderNumber.FieldName = "RequestHeaderNumber";
            this.colRequestHeaderNumber.Name = "colRequestHeaderNumber";
            this.colRequestHeaderNumber.Visible = true;
            this.colRequestHeaderNumber.VisibleIndex = 1;
            // 
            // colPoNumber
            // 
            this.colPoNumber.FieldName = "PoNumber";
            this.colPoNumber.Name = "colPoNumber";
            this.colPoNumber.Visible = true;
            this.colPoNumber.VisibleIndex = 2;
            // 
            // colApplicationPerson
            // 
            this.colApplicationPerson.FieldName = "ApplicationPerson";
            this.colApplicationPerson.Name = "colApplicationPerson";
            this.colApplicationPerson.Visible = true;
            this.colApplicationPerson.VisibleIndex = 3;
            // 
            // colCreatePerson
            // 
            this.colCreatePerson.FieldName = "CreatePerson";
            this.colCreatePerson.Name = "colCreatePerson";
            this.colCreatePerson.Visible = true;
            this.colCreatePerson.VisibleIndex = 4;
            // 
            // colFollowupPerson
            // 
            this.colFollowupPerson.FieldName = "FollowupPerson";
            this.colFollowupPerson.Name = "colFollowupPerson";
            this.colFollowupPerson.Visible = true;
            this.colFollowupPerson.VisibleIndex = 5;
            // 
            // colAuditor
            // 
            this.colAuditor.FieldName = "Auditor";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.Visible = true;
            this.colAuditor.VisibleIndex = 6;
            // 
            // colAuditDepart
            // 
            this.colAuditDepart.FieldName = "AuditDepart";
            this.colAuditDepart.Name = "colAuditDepart";
            this.colAuditDepart.Visible = true;
            this.colAuditDepart.VisibleIndex = 7;
            // 
            // colTotalApplied
            // 
            this.colTotalApplied.FieldName = "TotalApplied";
            this.colTotalApplied.Name = "colTotalApplied";
            this.colTotalApplied.Visible = true;
            this.colTotalApplied.VisibleIndex = 8;
            // 
            // colTotalConfirmed
            // 
            this.colTotalConfirmed.FieldName = "TotalConfirmed";
            this.colTotalConfirmed.Name = "colTotalConfirmed";
            this.colTotalConfirmed.Visible = true;
            this.colTotalConfirmed.VisibleIndex = 9;
            // 
            // colAuditStatus
            // 
            this.colAuditStatus.FieldName = "AuditStatus";
            this.colAuditStatus.Name = "colAuditStatus";
            this.colAuditStatus.Visible = true;
            this.colAuditStatus.VisibleIndex = 10;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 11;
            // 
            // colCompletePercentage
            // 
            this.colCompletePercentage.FieldName = "CompletePercentage";
            this.colCompletePercentage.Name = "colCompletePercentage";
            this.colCompletePercentage.Visible = true;
            this.colCompletePercentage.VisibleIndex = 12;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 13;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 14;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "拷贝", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // colCopy
            // 
            this.colCopy.Name = "colCopy";
            // 
            // PurchaseApplicationCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "PurchaseApplicationCtrl";
            this.Size = new System.Drawing.Size(1585, 455);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseApplicationHeaderViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion
        private GridView gridView1;
        private System.Windows.Forms.BindingSource purchaseApplicationHeaderViewModelBindingSource;
        protected DevExpress.XtraGrid.Columns.GridColumn colApplicationNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestHeaderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPoNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatePerson;
        private DevExpress.XtraGrid.Columns.GridColumn colFollowupPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditDepart;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalApplied;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalConfirmed;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCompletePercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCopy;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}
