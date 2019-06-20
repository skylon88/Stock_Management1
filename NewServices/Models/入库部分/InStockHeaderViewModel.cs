using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.入库部分
{
    public class InStockHeaderViewModel
    {
        [Display(Name = "入库单号")]
        public string InStockNumber { get; set; }
        [Display(Name = "需求编号")]
        public string RequestNumber { get; set; }
        [Display(Name = "采购单号")]
        public string PurchaseNumber { get; set; }
        [Display(Name = "PO编号")]
        public string PoNumber { get; set; }
        [Display(Name = "合同编号")]
        public string ContractNumber { get; set; }
        [Display(Name = "供应商")]
        public string SupplierName { get; set; }
        [Display(Name = "制表科室")]
        public string ApplicationDept { get; set; } //制表科室
        [Display(Name = "制表人")]
        public string CreatePerson { get; set; } //制表人
        [Display(Name = "审核人")]
        public string Auditor { get; set; } //审核人
        [Display(Name = "审核部门")]
        public string AuditDepart { get; set; } //审核部门
        [Display(Name = "需求类别"), EnumDataType(typeof(RequestCategoriesEnum))]
        public RequestCategoriesEnum InStockCategory { get; set; }

        [Display(Name = "入库详细")]
        public BindingList<InStockViewModel> InStockViewModels { get; set; }

        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
    }
}
