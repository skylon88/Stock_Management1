

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace App.Views.采购部分
{
    public partial class SignleGroupSelectGridView : GridView
    {
        public SignleGroupSelectGridView() : this(null) { }
        public SignleGroupSelectGridView(GridControl grid) : base(grid)
        {
            
        }

        protected override void ChangeGroupRowSelection(int rowHandle)
        {
            int state = GetGroupSelectionState(rowHandle);
            if (state != 2)
            {
                this.ClearSelection();
            }
            base.ChangeGroupRowSelection(rowHandle);
        }

        bool locked;

        public override void SelectRow(int rowHandle)
        {
            if (!IsMultiSelect) return;

            base.SelectRow(rowHandle);
            if (locked) return;
            locked = true;
            if (this.GetParentRowHandle(rowHandle) < 0 && this.GetParentRowHandle(rowHandle) != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                this.ClearSelection();
                this.SelectAllGroupRows(this.GetParentRowHandle(rowHandle));
            }
            locked = false;

        }

        public override void UnselectRow(int rowHandle)
        {
            if (!IsMultiSelect) return;
            base.UnselectRow(rowHandle);
            if (locked) return;
            locked = true;
            if (this.GetParentRowHandle(rowHandle) < 0 && this.GetParentRowHandle(rowHandle) != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                this.ClearSelection();
                this.UnselectAllGroupRows(this.GetParentRowHandle(rowHandle));
            }
            locked = false;
        }
    }
}
