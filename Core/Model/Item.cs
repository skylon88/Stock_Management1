using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class Item
    {
        public Guid ItemId { get; set; }

        public int SerialNumber { get; set; }
        public string Code { get; set; } // 编号
        public string Status { get; set; } //状态
        public string Category { get; set; } //物品类别
        public string ProjectCategory { get; set; } //所属转项类
        public string SubCategory { get; set; } //物品种类
        public string BigCategory { get; set; } //物品大类
        public string SmallCategory { get; set; } //物品小类
        public string DetailCategory { get; set; } //物品细类
        public string AdjustCategory { get; set; } //调整类
        public string Attribute { get; set; } //物品属性
        public string Property { get; set; } //资产类型
        public string ChineseName { get; set; } //中文名
        public string EnglishName { get; set; } //英文名
        public byte[] Picture { get; set; } //图片
        public string Brand { get; set; } //品牌
        public string Model { get; set; } //型号
        public string Specification { get; set; } //规格
        public string Dimension { get; set; } //尺寸

        public string Length { get; set; } // 长
        public string Width { get; set; } //宽
        public string Height { get; set; } //高

        public string Unit { get; set; } //单位
        public double Price { get; set; } //单价

        public string Package { get; set; } //包装数量
        public string PackageLength { get; set; } //包装长
        public string PackageWidth { get; set; } //包装宽
        public string PackageHeight { get; set; } //包装高
        public string Detail { get; set; } //详细信息链接

        public double Max { get; set; } //库存上限
        public double Min { get; set; } //库存下限

        public string FirstSupplier { get; set; } // 一级供应商
        public string SecondSupplier { get; set; } // 二级供应商
        public string ThirdSupplier { get; set; } // 三级供应商
        public string CostCategory { get; set; } //成本类别
        public string Comments { get; set; }
        public string ArrangeOrder { get; set; } //摆放顺序
        public string ArrangePosition { get; set; } //摆放位置
        public DateTime UpdateDate { get; set; }

        public string DefaultPositionName { get; set; }

        public ICollection<Position> Positions { get; set; }

    }
}
