using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewServices.Models.管理
{
    public class ItemViewModel
    {
        public Guid ItemId { get; set; }
        [Display(Name = "序号")]
        public int SerialNumber { get; set; }
        [Display(Name = "物品编号")]
        public string Code { get; set; } // 编号
        [Display(Name = "状态")]
        public string Status { get; set; } //状态
        [Display(Name = "物品类别")]
        public string Category { get; set; } //物品类别
        [Display(Name = "所属转项类")]
        public string ProjectCategory { get; set; } //所属转项类
        [Display(Name = "物品种类")]
        public string SubCategory { get; set; } //物品种类
        [Display(Name = "物品大类")]
        public string BigCategory { get; set; } //物品大类
        [Display(Name = "物品小类")]
        public string SmallCategory { get; set; } //物品小类
        [Display(Name = "物品细类")]
        public string DetailCategory { get; set; } //物品细类
        [Display(Name = "调整类")]
        public string AdjustCategory { get; set; } //调整类
        [Display(Name = "物品属性")]
        public string Attribute { get; set; } //物品属性
        [Display(Name = "资产类型")]
        public string Property { get; set; } //资产类型
        [Display(Name = "中文名")]
        public string ChineseName { get; set; } //中文名
        [Display(Name = "英文名")]
        public string EnglishName { get; set; } //英文名
        [Display(Name = "图片")]
        public byte[] Picture { get; set; } //图片
        [Display(Name = "品牌")]
        public string Brand { get; set; } //品牌
        [Display(Name = "型号")]
        public string Model { get; set; } //型号
        [Display(Name = "规格")]
        public string Specification { get; set; } //规格
        [Display(Name = "尺寸")]
        public string Dimension { get; set; } //尺寸
        [Display(Name = "长")]
        public string Length { get; set; } // 长
        [Display(Name = "宽")]
        public string Width { get; set; } //宽
        [Display(Name = "高")]
        public string Height { get; set; } //高
        [Display(Name = "单位")]
        public string Unit { get; set; } //单位
        [Display(Name = "单价")]
        public double Price { get; set; } //单价
        [Display(Name = "包装数量")]
        public string Package { get; set; } //包装数量
        [Display(Name = "包装长")]
        public string PackageLength { get; set; } //包装长
        [Display(Name = "包装宽")]
        public string PackageWidth { get; set; } //包装宽
        [Display(Name = "包装高")]
        public string PackageHeight { get; set; } //包装高
        [Display(Name = "详细")]
        public string Detail { get; set; } //详细信息链接
        [Display(Name = "库存数")]
        public double TotalInStorage { get; set; } //库存数
        [Display(Name = "库存上限")]
        public double Max { get; set; } //库存上限
        [Display(Name = "库存下限")]
        public double Min { get; set; } //库存下限
        [Display(Name = "一级供应商")]
        public string FirstSupplier { get; set; } // 一级供应商
        [Display(Name = "二级供应商")]
        public string SecondSupplier { get; set; } // 二级供应商
        [Display(Name = "三级供应商")]
        public string ThirdSupplier { get; set; } // 三级供应商
        [Display(Name = "成本类别")]
        public string CostCategory { get; set; } //成本类别
        [Display(Name = "摆放顺序")]
        public string ArrangeOrder { get; set; } //摆放顺序
        [Display(Name = "摆放位置")]
        public string ArrangePosition { get; set; } //摆放位置
        [Display(Name = "备注")]
        public string Comments { get; set; }
        [Display(Name = "更新时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }

        public BindingList<PositionViewModel> PositionViewModels { get; set; }
    }

    public class PositionViewModel
    {
        public Guid Id;
        public Guid ItemId;
        [Display(Name = "库存位置")]
        public string PositionName { get; set; }
        [Display(Name = "区")]
        public string Area { get; set; }
        [Display(Name = "位置")]
        public string Location { get; set; }
        [Display(Name = "库存数量")]
        public double Total { get; set; }

        [Display(Name = "最近一次更新时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }
        [Display(Name = "最近一次更新入库单号")]
        public string LatestInStockNumber { get; set; }
        [Display(Name = "备注")]
        public string Comment { get; set; }
    }
}
