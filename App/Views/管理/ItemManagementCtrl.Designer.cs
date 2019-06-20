using NewServices.Models.管理;

namespace App.Views.管理
{
    sealed partial class ItemManagementCtrl
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.itemViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerialNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBigCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSmallCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdjustCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttribute = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProperty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChineseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnglishName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPicture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBrand = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpecification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDimension = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPackage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalInStorage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSecondSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThirdSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArrangeOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArrangePosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.itemViewModelBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            //this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1001, 407);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemId,
            this.colSerialNumber,
            this.colCode,
            this.colStatus,
            this.colCategory,
            this.colProjectCategory,
            this.colSubCategory,
            this.colBigCategory,
            this.colSmallCategory,
            this.colDetailCategory,
            this.colAdjustCategory,
            this.colAttribute,
            this.colProperty,
            this.colChineseName,
            this.colEnglishName,
            this.colPicture,
            this.colBrand,
            this.colModel,
            this.colSpecification,
            this.colDimension,
            this.colUnit,
            this.colPrice,
            this.colPackage,
            this.colTotalInStorage,
            this.colDetail,
            this.colMax,
            this.colMin,
            this.colFirstSupplier,
            this.colSecondSupplier,
            this.colThirdSupplier,
            this.colCostCategory,
            this.colArrangeOrder,
            this.colArrangePosition,
            this.colComments,
            this.colUpdateDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.RowCellStyle += gridView_RowCellStyle;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            this.gridView1.MasterRowGetRelationDisplayCaption += new DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventHandler(this.gridView1_MasterRowGetRelationDisplayCaption);
            // 
            // itemViewModelBindingSource
            // 
            this.itemViewModelBindingSource.DataSource = typeof(ItemViewModel);
            // 
            // colItemId
            // 
            this.colItemId.FieldName = "ItemId";
            this.colItemId.Name = "colItemId";
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.FieldName = "SerialNumber";
            this.colSerialNumber.Name = "colSerialNumber";
            this.colSerialNumber.Visible = true;
            this.colSerialNumber.VisibleIndex = 0;
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 1;
            // 
            // colCategory
            // 
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 2;
            // 
            // colProjectCategory
            // 
            this.colProjectCategory.FieldName = "ProjectCategory";
            this.colProjectCategory.Name = "colProjectCategory";
            this.colProjectCategory.Visible = true;
            this.colProjectCategory.VisibleIndex = 3;
            // 
            // colSubCategory
            // 
            this.colSubCategory.FieldName = "SubCategory";
            this.colSubCategory.Name = "colSubCategory";
            this.colSubCategory.Visible = true;
            this.colSubCategory.VisibleIndex = 4;
            // 
            // colBigCategory
            // 
            this.colBigCategory.FieldName = "BigCategory";
            this.colBigCategory.Name = "colBigCategory";
            this.colBigCategory.Visible = true;
            this.colBigCategory.VisibleIndex = 5;
            // 
            // colSmallCategory
            // 
            this.colSmallCategory.FieldName = "SmallCategory";
            this.colSmallCategory.Name = "colSmallCategory";
            this.colSmallCategory.Visible = true;
            this.colSmallCategory.VisibleIndex = 6;
            // 
            // colDetailCategory
            // 
            this.colDetailCategory.FieldName = "DetailCategory";
            this.colDetailCategory.Name = "colDetailCategory";
            this.colDetailCategory.Visible = true;
            this.colDetailCategory.VisibleIndex = 7;
            // 
            // colAdjustCategory
            // 
            this.colAdjustCategory.FieldName = "AdjustCategory";
            this.colAdjustCategory.Name = "colAdjustCategory";
            this.colAdjustCategory.Visible = true;
            this.colAdjustCategory.VisibleIndex = 8;
            // 
            // colAttribute
            // 
            this.colAttribute.FieldName = "Attribute";
            this.colAttribute.Name = "colAttribute";
            this.colAttribute.Visible = true;
            this.colAttribute.VisibleIndex = 9;
            // 
            // colProperty
            // 
            this.colProperty.FieldName = "Property";
            this.colProperty.Name = "colProperty";
            this.colProperty.Visible = true;
            this.colProperty.VisibleIndex = 10;
            // 
            // colChineseName
            // 
            this.colChineseName.FieldName = "ChineseName";
            this.colChineseName.Name = "colChineseName";
            this.colChineseName.Visible = true;
            this.colChineseName.VisibleIndex = 11;
            // 
            // colEnglishName
            // 
            this.colEnglishName.FieldName = "EnglishName";
            this.colEnglishName.Name = "colEnglishName";
            this.colEnglishName.Visible = true;
            this.colEnglishName.VisibleIndex = 12;
            // 
            // colPicture
            // 
            this.colPicture.FieldName = "Picture";
            this.colPicture.Name = "colPicture";
            this.colPicture.Visible = true;
            this.colPicture.VisibleIndex = 13;
            // 
            // colBrand
            // 
            this.colBrand.FieldName = "Brand";
            this.colBrand.Name = "colBrand";
            this.colBrand.Visible = true;
            this.colBrand.VisibleIndex = 14;
            // 
            // colModel
            // 
            this.colModel.FieldName = "Model";
            this.colModel.Name = "colModel";
            this.colModel.Visible = true;
            this.colModel.VisibleIndex = 15;
            // 
            // colSpecification
            // 
            this.colSpecification.FieldName = "Specification";
            this.colSpecification.Name = "colSpecification";
            this.colSpecification.Visible = true;
            this.colSpecification.VisibleIndex = 16;
            // 
            // colDimension
            // 
            this.colDimension.FieldName = "Dimension";
            this.colDimension.Name = "colDimension";
            this.colDimension.Visible = true;
            this.colDimension.VisibleIndex = 17;
            // 
            // colUnit
            // 
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 18;
            // 
            // colPrice
            // 
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 19;
            // 
            // colPackage
            // 
            this.colPackage.FieldName = "Package";
            this.colPackage.Name = "colPackage";
            this.colPackage.Visible = true;
            this.colPackage.VisibleIndex = 20;
            // 
            // colDetail
            // 
            this.colDetail.FieldName = "Detail";
            this.colDetail.Name = "colDetail";
            this.colDetail.Visible = true;
            this.colDetail.VisibleIndex = 21;
            // 
            // colMax
            // 
            this.colTotalInStorage.FieldName = "TotalInStorage";
            this.colTotalInStorage.Name = "colTotalInStorage";
            this.colTotalInStorage.Visible = true;
            this.colTotalInStorage.VisibleIndex = 22;
            // 
            // colMax
            // 
            this.colMax.FieldName = "Max";
            this.colMax.Name = "colMax";
            this.colMax.Visible = true;
            this.colMax.VisibleIndex = 23;
            // 
            // colMin
            // 
            this.colMin.FieldName = "Min";
            this.colMin.Name = "colMin";
            this.colMin.Visible = true;
            this.colMin.VisibleIndex = 24;
            // 
            // colFirstSupplier
            // 
            this.colFirstSupplier.FieldName = "FirstSupplier";
            this.colFirstSupplier.Name = "colFirstSupplier";
            this.colFirstSupplier.Visible = true;
            this.colFirstSupplier.VisibleIndex = 25;
            // 
            // colSecondSupplier
            // 
            this.colSecondSupplier.FieldName = "SecondSupplier";
            this.colSecondSupplier.Name = "colSecondSupplier";
            this.colSecondSupplier.Visible = true;
            this.colSecondSupplier.VisibleIndex = 26;
            // 
            // colThirdSupplier
            // 
            this.colThirdSupplier.FieldName = "ThirdSupplier";
            this.colThirdSupplier.Name = "colThirdSupplier";
            this.colThirdSupplier.Visible = true;
            this.colThirdSupplier.VisibleIndex = 27;
            // 
            // colCostCategory
            // 
            this.colCostCategory.FieldName = "CostCategory";
            this.colCostCategory.Name = "colCostCategory";
            this.colCostCategory.Visible = true;
            this.colCostCategory.VisibleIndex = 28;
            // 
            // colArrangeOrder
            // 
            this.colArrangeOrder.FieldName = "ArrangeOrder";
            this.colArrangeOrder.Name = "colArrangeOrder";
            this.colArrangeOrder.Visible = true;
            this.colArrangeOrder.VisibleIndex = 29;
            // 
            // colArrangePosition
            // 
            this.colArrangePosition.FieldName = "ArrangePosition";
            this.colArrangePosition.Name = "colArrangePosition";
            this.colArrangePosition.Visible = true;
            this.colArrangePosition.VisibleIndex = 30;
            // 
            // colComments
            // 
            this.colComments.FieldName = "Comments";
            this.colComments.Name = "colComments";
            this.colComments.Visible = true;
            this.colComments.VisibleIndex = 31;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 32;
            // 
            // ItemManagementCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "ItemManagementCtrl";
            this.Size = new System.Drawing.Size(1001, 407);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource itemViewModelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colSubCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colBigCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colSmallCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colAdjustCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colAttribute;
        private DevExpress.XtraGrid.Columns.GridColumn colProperty;
        private DevExpress.XtraGrid.Columns.GridColumn colChineseName;
        private DevExpress.XtraGrid.Columns.GridColumn colEnglishName;
        private DevExpress.XtraGrid.Columns.GridColumn colPicture;
        private DevExpress.XtraGrid.Columns.GridColumn colBrand;
        private DevExpress.XtraGrid.Columns.GridColumn colModel;
        private DevExpress.XtraGrid.Columns.GridColumn colSpecification;
        private DevExpress.XtraGrid.Columns.GridColumn colDimension;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPackage;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalInStorage;
        private DevExpress.XtraGrid.Columns.GridColumn colDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colMax;
        private DevExpress.XtraGrid.Columns.GridColumn colMin;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colSecondSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colThirdSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colComments;
        private DevExpress.XtraGrid.Columns.GridColumn colArrangeOrder;
        private DevExpress.XtraGrid.Columns.GridColumn colArrangePosition;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
    }
}
