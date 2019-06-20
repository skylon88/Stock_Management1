

namespace Core.Enum
{
    public enum RequestCategoriesEnum
    {

        材料需求 = 1,
        工具借出 = 2,
        工具维修 = 3,
        工程车维修 = 4,
        员工补给 = 5,
        工程车补给 = 6,
        办公用品 = 7,
        物品退回 = 8,
        采购退货 = 9,
        购入需求 = 10
    }

    public enum ApplicationCategoriesEnum
    {

        采购申请汇总 = 1,
        退货申请汇总 = 2
    }

    public enum PurchaseCategoriesEnum
    {

        采购单 = 1,
        退货单 = 2
    }

    public enum RequestReason
    {
        测量,
        反工,
        丢失,
        送错,
        损坏,
        收尾
    }

    public enum PriorityEnum
    {
        三级 = 3,
        二级 = 2,
        一级 = 1
    }
    public enum ProcessStatusEnum {
        需求建立 = 1,
        申请审核中 = 2,
        采购中 = 3,
        采购完成 = 4,
        采购入库 = 5,
        已出库 = 6,
        
    
        工程车维修入库 = 7,
        维修中 = 8,
        维修完成 = 9,
        工程车维修出库 = 10,
        
        借出出库 = 11,
        归还入库 = 12,

        补给出库 = 13,

        退回入库 = 14,

        退货中 = 15,
        退货申请完成 = 16,
        退货出库 = 17,

        报废出库 = 18,
        取消 = 19,




    }

    public enum AuditStatusEnum
    {
        未审批,
        已审批 
    }

    public enum InStockCategoryEnum
    {
        物品归还 = 1,
        物品维修 = 2,
        采购入库 = 3,
        材料退回 = 4,
        顾客寄存 = 5,
    }

    public enum PurchaseCategoryEnum
    {
        订货,
        采购
    }
    public enum DeliveryCategoryEnum
    {
        送货,
        自提
    }

    public enum ReportNameEnum
    {
        采购申请单,
        采购单,
        入库单,
        出库单,
        物品盘点,
        捡货
    }


    public enum SupplierType
    {
       公寓,
       独立屋
        
    }

    public enum LockStatusEnum
    {
        未准备,
        已准备
    }

}