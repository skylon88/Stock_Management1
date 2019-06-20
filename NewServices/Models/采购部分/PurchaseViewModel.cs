using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.采购部分
{
    public class PurchaseViewModel
    {
        public Guid PurchaseId { get; set; }
        public Guid RequestId { get; set; }
        [Display(Name = "采购单号")]
        public string PurchaseNumber { get; set; }
        [Display(Name = "采购申请编号")]
        public string ApplicationNumber { get; set; }
        public string PoNumber { get; set; }
        //[Display(Name = "订单号码")]
        //public string BookingNumber { get; set; }
        [Display(Name = "供应商")]
        public string SupplierCode { get; set; }
        [Display(Name = "物品类别")]
        public string Category { get; set; }
        [Display(Name = "物品名称")]
        public string Name { get; set; }
        [Display(Name = "物品编码")]
        public string Code { get; set; }
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
        [Display(Name = "默认单价")]
        public double DefaultPrice { get; set; } //默认单价
        [Display(Name = "购买单价")]
        public double Price { get; set; } //当前购买单价
        [Display(Name = "数量")]
        public double PurchaseTotal { get; set; } //数量
        [Display(Name = "合计")]
        public double TotalPrice { get; set; } //合计
        [Display(Name = "到货数量")]
        public double ReadyForInStock { get; set; } //到货数量
        [Display(Name = "最后到货日期"), DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime? LastDeliveryDate { get; set; }
        [Display(Name = "对错数量")]
        public double CorrectionTotal { get; set; }
        [Display(Name = "状态")]
        public ProcessStatusEnum Status { get; set; }
        [Display(Name = "删除")]
        public bool IsDeleted { get; set; }
        [Display(Name = "已入库数量")]
        public double AlreadyInStock { get; set; } //已入库数量

        [Display(Name = "库位")]
        public string Position { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }
        [Display(Name = "提货日期"), DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }

        public bool IsPriceChange { get; set; }

        public Guid ItemId { get; set; }

    }
}
