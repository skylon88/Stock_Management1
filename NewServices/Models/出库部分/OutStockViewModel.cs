using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.出库部分
{
    public class OutStockViewModel
    {
        public string OutStockNumber { get; set; }
        [Display(Name = "物品名称")]
        public string Name { get; set; }
        [Display(Name = "物品编码")]
        public string Code { get; set; }
        [Display(Name = "库位")]
        public string PositionName { get; set; }
        [Display(Name = "品牌")]
        public string Brand { get; set; } //品牌
        [Display(Name = "型号")]
        public string Model { get; set; } //型号
        [Display(Name = "规格")]
        public string Specification { get; set; } //规格
        [Display(Name = "尺寸")]
        public string Dimension { get; set; } //尺寸
        [Display(Name = "出库数量")]
        public int Total { get; set; }
        [Display(Name = "单位")]
        public string Unit { get; set; } //单位
        [Display(Name = "单价")]
        public double Price { get; set; } //单价
        [Display(Name = "合计")]
        public double TotalPrice { get; set; } //合计
        [Display(Name = "备注")]
        public string Note { get; set; }

        [Display(Name = "跟进状态")]
        public ProcessStatusEnum ProcessStatus { get; set; }

        [Display(Name = "更新时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }

        public Guid RequestId { get; set; }

        public RequestCategoriesEnum Type { get; set; }
    }
}
