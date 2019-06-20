using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.采购部分
{

    public class PurchaseApplicationViewModel 
    {
        public Guid PurchaseApplicationId { get; set; }

        public Guid RequestId { get; set; }

        [Display(Name = "序号")]
        public int SerialNumber { get; set; }

        [Display(Name = "采购申请号")]
        public string ApplicationNumber { get; set; }

        [Display(Name = "需求编号")]
        public string RequestNumber { get; set; }

        [Display(Name = "采购单号")]
        public string SelectedPurchaseNumber { get; set; }

        [Display(Name = "申请类别")]
        public RequestCategoriesEnum RequestCategory { get; set; }

        [Display(Name = "PO编号")]
        public string PoNumber { get; set; }
        [Display(Name = "合同编号")]
        public string ContractNo { get; set; }
        [Display(Name = "物品名称")]
        public string Name { get; set; }
        [Display(Name = "物品编号")]
        public string Code { get; set; }
        [Display(Name = "供应商")]
        public string SupplierName { get; set; }
        [Display(Name = "物品类别")]
        public string Category { get; set; } //物品类别

        [Display(Name = "品牌")]
        public string Brand { get; set; } //品牌
        [Display(Name = "型号")]
        public string Model { get; set; } //型号
        [Display(Name = "规格")]
        public string Specification { get; set; } //规格
        [Display(Name = "尺寸")]
        public string Dimension { get; set; } //尺寸
        [Display(Name = "单位")]
        public string Unit { get; set; } //单位

        [Display(Name = "单价")]
        public double CurrentPurchasePrice { get; set; }
        [Display(Name = "申请数量")]
        public double TotalApplied { get; set; }
        [Display(Name = "审批数量")]
        public double TotalConfirmed { get; set; }
        [Display(Name = "审批状态"), EnumDataType(typeof(AuditStatusEnum))]
        public AuditStatusEnum AuditStatus { get; set; }
        [Display(Name = "采购等级"), EnumDataType(typeof(PriorityEnum))]
        public PriorityEnum Priority { get; set; }

        [Display(Name = "状态")]
        public ProcessStatusEnum ProcessStatus { get; set; }
        [Display(Name = "备注")]
        public string Note { get; set; }
        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }

        public Guid ItemId { get; set; }

    }

}