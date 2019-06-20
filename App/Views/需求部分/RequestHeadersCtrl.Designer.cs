using System;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using NewServices.Models.需求部分;

namespace App.Views.需求部分
{
    partial class RequestHeadersCtrl
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
            this.requestHeaderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRequestHeaderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompletePercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPONumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicationPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatePerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFollowupPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditDepart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLockStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestHeaderViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.requestHeaderViewModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            //this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1165, 824);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // requestHeaderViewModelBindingSource
            // 
            this.requestHeaderViewModelBindingSource.DataSource = typeof(RequestHeaderViewModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRequestHeaderNumber,
            this.colRequestCategory,
            this.colPONumber,
            this.colContractId,
            this.colContractAddress,
            this.colApplicationPerson,
            this.colCreatePerson,
            this.colFollowupPerson,
            this.colAuditor,
            this.colAuditDepart,
            this.colTotal,
            this.colLockStatus,
            this.colStatus,
            this.colCompletePercentage,
            this.colCreateDate,
            this.colUpdateDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            //this.gridView1.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            this.gridView1.MasterRowExpanding += new DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventHandler(this.gridView1_MasterRowExpanding);
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            this.gridView1.MasterRowGetRelationDisplayCaption += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gridView1_MasterRowGetRelationDisplayCaption);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.RowCellStyle += gridView_RowCellStyle;
            // 
            // colRequestHeaderNumber
            // 
            this.colRequestHeaderNumber.FieldName = "RequestHeaderNumber";
            this.colRequestHeaderNumber.Name = "colRequestHeaderNumber";
            this.colRequestHeaderNumber.Visible = true;
            this.colRequestHeaderNumber.VisibleIndex = 1;
            // 
            // colRequestCategory
            // 
            this.colRequestCategory.FieldName = "RequestCategory";
            this.colRequestCategory.Name = "colRequestCategory";
            this.colRequestCategory.Visible = true;
            this.colRequestCategory.VisibleIndex = 2;
            // 
            // colPONumber
            // 
            this.colPONumber.FieldName = "PoNumber";
            this.colPONumber.Name = "colPoNumber";
            this.colPONumber.Visible = true;
            this.colPONumber.VisibleIndex = 3;
            // 
            // colContractId
            // 
            this.colContractId.FieldName = "ContractId";
            this.colContractId.Name = "colContractId";
            this.colContractId.Visible = true;
            this.colContractId.VisibleIndex = 4;
            // 
            // colContractAddress
            // 
            this.colContractAddress.FieldName = "ContractAddress";
            this.colContractAddress.Name = "colContractAddress";
            this.colContractAddress.Visible = true;
            this.colContractAddress.VisibleIndex = 5;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 5;
            // 
            // colApplicationPerson
            // 
            this.colApplicationPerson.FieldName = "ApplicationPerson";
            this.colApplicationPerson.Name = "colApplicationPerson";
            this.colApplicationPerson.Visible = true;
            this.colApplicationPerson.VisibleIndex = 7;
            // 
            // colCreatePerson
            // 
            this.colCreatePerson.FieldName = "CreatePerson";
            this.colCreatePerson.Name = "colCreatePerson";
            this.colCreatePerson.Visible = true;
            this.colCreatePerson.VisibleIndex = 8;
            // 
            // colFollowupPerson
            // 
            this.colFollowupPerson.FieldName = "FollowupPerson";
            this.colFollowupPerson.Name = "colFollowupPerson";
            this.colFollowupPerson.Visible = true;
            this.colFollowupPerson.VisibleIndex = 9;
            // 
            // colAuditor
            // 
            this.colAuditor.FieldName = "Auditor";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.Visible = true;
            this.colAuditor.VisibleIndex = 10;
            // 
            // colAuditDepart
            // 
            this.colAuditDepart.FieldName = "AuditDepart";
            this.colAuditDepart.Name = "colAuditDepart";
            this.colAuditDepart.Visible = true;
            this.colAuditDepart.VisibleIndex = 11;
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 12;

            // 
            // colTotal
            // 
            this.colLockStatus.FieldName = "LockStatus";
            this.colLockStatus.Name = "colLockStatus";
            this.colLockStatus.Visible = true;
            this.colLockStatus.VisibleIndex = 13;

            // 
            // colCompletePercentage
            // 
            this.colCompletePercentage.FieldName = "CompletePercentage";
            this.colCompletePercentage.Name = "colCompletePercentage";
            this.colCompletePercentage.Visible = true;
            this.colCompletePercentage.VisibleIndex = 14;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 15;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 16;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // RequestHeadersCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "RequestHeadersCtrl";
            this.Size = new System.Drawing.Size(1165, 824);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestHeaderViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource requestHeaderViewModelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestHeaderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestCategory;
        protected DevExpress.XtraGrid.Columns.GridColumn colPONumber;
        private DevExpress.XtraGrid.Columns.GridColumn colContractId;
        private DevExpress.XtraGrid.Columns.GridColumn colContractAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicationPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatePerson;
        private DevExpress.XtraGrid.Columns.GridColumn colFollowupPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditDepart;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colLockStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCompletePercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}
