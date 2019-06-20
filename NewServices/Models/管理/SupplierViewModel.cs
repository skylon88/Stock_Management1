using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.管理
{
    public class SupplierViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "供应商")]
        public string Name { get; set; }

        [Display(Name = "代码")]
        public string Code { get; set; }
        [Display(Name = "类型")]
        public SupplierType Type { get; set; }
        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "更新时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }
    }
}
