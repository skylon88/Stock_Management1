using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enum;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace App.Helper
{
    public static class RenderCommonHelper
    {
        public static void  SetStatusColor(ProcessStatusEnum processStatus, RowCellStyleEventArgs e)
        {
            switch (processStatus)
            {
                case ProcessStatusEnum.需求建立:
                    e.Appearance.BackColor = Color.SkyBlue;
                    break;
                case ProcessStatusEnum.申请审核中:
                    e.Appearance.BackColor = Color.YellowGreen;
                    break;
                case ProcessStatusEnum.采购中:
                    e.Appearance.BackColor = Color.Yellow;
                    break;
                case ProcessStatusEnum.退货中:
                    e.Appearance.BackColor = Color.Yellow;
                    break;
                case ProcessStatusEnum.采购完成:
                    e.Appearance.BackColor = Color.LawnGreen;
                    break;
                case ProcessStatusEnum.采购入库:
                    e.Appearance.BackColor = Color.Chocolate;
                    break;
                case ProcessStatusEnum.已出库:
                    e.Appearance.BackColor = Color.LightSeaGreen;
                    break;
                case ProcessStatusEnum.工程车维修入库:
                    e.Appearance.BackColor = Color.LawnGreen;
                    break;
                case ProcessStatusEnum.维修中:
                    e.Appearance.BackColor = Color.Yellow;
                    break;
                case ProcessStatusEnum.维修完成:
                    e.Appearance.BackColor = Color.LawnGreen;
                    break;
                case ProcessStatusEnum.工程车维修出库:
                    e.Appearance.BackColor = Color.LightSeaGreen;
                    break;
                case ProcessStatusEnum.借出出库:
                    e.Appearance.BackColor = Color.Yellow;
                    break;
                case ProcessStatusEnum.归还入库:
                    e.Appearance.BackColor = Color.LawnGreen;
                    break;
                case ProcessStatusEnum.补给出库:
                    e.Appearance.BackColor = Color.LawnGreen;
                    break;
                case ProcessStatusEnum.退货申请完成:
                    e.Appearance.BackColor = Color.LightSeaGreen;
                    break;
                case ProcessStatusEnum.退货出库:
                    e.Appearance.BackColor = Color.LawnGreen;
                    break;
                case ProcessStatusEnum.报废出库:
                    e.Appearance.BackColor = Color.Red;
                    break;
                case ProcessStatusEnum.取消:
                    e.Appearance.BackColor = Color.Red;
                    break;
            }
        }

        public static void SetCategoryColor(RequestCategoriesEnum requestCategoriesEnum, RowCellStyleEventArgs e)
        {
            switch (requestCategoriesEnum)
            {
                case RequestCategoriesEnum.材料需求:
                    e.Appearance.BackColor = Color.AntiqueWhite;
                    break;
                case RequestCategoriesEnum.工程车维修:
                    e.Appearance.BackColor = Color.Cornsilk;
                    break;
                case RequestCategoriesEnum.工具维修:
                    e.Appearance.BackColor = Color.LightSlateGray;
                    break;
                case RequestCategoriesEnum.工程车补给:
                    e.Appearance.BackColor = Color.LemonChiffon;
                    break;
                case RequestCategoriesEnum.员工补给:
                    e.Appearance.BackColor = Color.LavenderBlush;
                    break;
                case RequestCategoriesEnum.物品退回:
                    e.Appearance.BackColor = Color.MintCream;
                    break;
                case RequestCategoriesEnum.采购退货:
                    e.Appearance.BackColor = Color.MistyRose;
                    break;
                case RequestCategoriesEnum.工具借出:
                    e.Appearance.BackColor = Color.Honeydew;
                    break;
            }
        }
        public static void SetColNotEditable(GridColumn col)
        {
            col.OptionsColumn.AllowEdit = false;
            col.OptionsColumn.AllowFocus = false;
        }

        public static void SetColEditable(GridColumn col)
        {
            col.OptionsColumn.AllowEdit = true;
            col.OptionsColumn.AllowFocus = true;
        }
    }
}
