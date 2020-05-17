namespace App.Views.管理
{
    partial class UnitManagementCtrl
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
            this.unitModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefaultUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConvertToUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFactor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsGeneral = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.unitModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl1_EmbeddedNavigator_ButtonClick);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Size = new System.Drawing.Size(2099, 934);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // unitModelBindingSource
            // 
            this.unitModelBindingSource.DataSource = typeof(Core.Model.UnitModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colItemName,
            this.colDefaultUnit,
            this.colConvertToUnit,
            this.colFactor,
            this.colIsGeneral});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.NewItemRowText = "添加新单位转换";
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            //this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            //this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.RowDeleting += new DevExpress.Data.RowDeletingEventHandler(this.gridView1_RowDeleting);
            this.gridView1.RowDeleted += new DevExpress.Data.RowDeletedEventHandler(this.gridView1_RowDeleted);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.MinWidth = 35;
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            this.colId.Width = 131;
            // 
            // colItemName
            // 
            this.colItemName.FieldName = "ItemName";
            this.colItemName.MinWidth = 35;
            this.colItemName.Name = "colItemName";
            this.colItemName.Caption = "物品编号";
            this.colItemName.Visible = true;
            this.colItemName.VisibleIndex = 1;
            this.colItemName.Width = 131;
            // 
            // colDefaultUnit
            // 
            this.colDefaultUnit.FieldName = "DefaultUnit";
            this.colDefaultUnit.MinWidth = 35;
            this.colDefaultUnit.Name = "colDefaultUnit";
            this.colDefaultUnit.Caption = "默认单位";
            this.colDefaultUnit.Visible = true;
            this.colDefaultUnit.VisibleIndex = 2;
            this.colDefaultUnit.Width = 131;
            // 
            // colConvertToUnit
            // 
            this.colConvertToUnit.FieldName = "ConvertToUnit";
            this.colConvertToUnit.MinWidth = 35;
            this.colConvertToUnit.Name = "colConvertToUnit";
            this.colConvertToUnit.Caption = "转换单位";
            this.colConvertToUnit.Visible = true;
            this.colConvertToUnit.VisibleIndex = 3;
            this.colConvertToUnit.Width = 131;
            // 
            // colFactor
            // 
            this.colFactor.FieldName = "Factor";
            this.colFactor.MinWidth = 35;
            this.colFactor.Caption = "转换率";
            this.colFactor.Name = "colFactor";
            this.colFactor.Visible = true;
            this.colFactor.VisibleIndex = 4;
            this.colFactor.Width = 131;
            // 
            // colIsGeneral
            // 
            this.colIsGeneral.FieldName = "IsGeneral";
            this.colIsGeneral.MinWidth = 35;
            this.colIsGeneral.Name = "colIsGeneral";
            this.colIsGeneral.Caption = "通用";
            this.colIsGeneral.Visible = true;
            this.colIsGeneral.VisibleIndex = 5;
            this.colIsGeneral.Width = 131;
            // 
            // UnitManagementCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UnitManagementCtrl";
            this.Size = new System.Drawing.Size(2099, 934);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource unitModelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colDefaultUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colConvertToUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colFactor;
        private DevExpress.XtraGrid.Columns.GridColumn colIsGeneral;
    }
}
