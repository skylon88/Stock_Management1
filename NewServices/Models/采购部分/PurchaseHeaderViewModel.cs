using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.采购部分
{
    public class PurchaseHeaderViewModel
    {
        [Display(Name = "采购类型")]
        public RequestCategoriesEnum PurchaseType { get; set; }
        [Display(Name = "采购单号")]
        public string PurchaseNumber { get; set; }
        [Display(Name = "PO编号")]
        public string PoNumber { get; set; }
        [Display(Name = "供应商")]
        public string SupplierName { get; set; }
        [Display(Name = "采购部门")]
        public string ApplicationDept { get; set; } //采购部门
        [Display(Name = "制表人")]
        public string CreatePerson { get; set; } //制表人
        [Display(Name = "审核人")]
        public string Auditor { get; set; } //审核人
        [Display(Name = "审核部门")]
        public string AuditDepart { get; set; } //审核部门
        [Display(Name = "采购等级"), EnumDataType(typeof(PriorityEnum))]
        public PriorityEnum Priority { get; set; }
        
        [Display(Name = "采购总金额")]
        public double TotalPrice { get; set; }
        [Display(Name = "完成率")]
        public decimal CompletePercentage { get; set; }
        [Display(Name = "采购类型")]
        public PurchaseCategoryEnum PurchaseCategory { get; set; }
        [Display(Name = "配送类型")]
        public DeliveryCategoryEnum DeliveryCategory { get; set; }

        [Display(Name = "提货日期"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime? DeliveryDate { get; set; }
        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }


        public BindingList<PurchaseViewModel> PurchaseViewModels { get; set; }
    }
}
