using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Enum;

namespace Services.Models
{
    public class RequestViewModel
    {
        //From Request table
        public Guid RequestId { get; set; }

        [Display(Name = "需求编号")]
        public string RequestNumber { get; set; }
        //public string RequestCategoryId { get; set; }

        [Display(Name = "需求类别"), EnumDataType(typeof(RequestCategoriesEnum))]
        public RequestCategoriesEnum RequestCategoryName { get; set; }
        public Guid ItemId { get; set; }
        [Display(Name = "物品名称")]
        public string Name { get; set; }
        [Display(Name = "物品编号")]
        public string Code { get; set; }
        [Display(Name = "数量")]
        public int Total { get; set; }
        [Display(Name = "单位")]
        public string Unit { get; set; }
        [Display(Name = "需求等级"),EnumDataType(typeof(PriorityEnum))]
        public PriorityEnum Priority { get; set; }
        [Display(Name = "需求原因")]
        public string Reason { get; set; } 
        [Display(Name = "其他需求原因")]
        public string OtherReason { get; set; }
        [Display(Name = "出库单编号")]
        public int StockNumber { get; set; }


        [Display(Name = "备注")]
        public string Note { get; set; }
        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        //For Item table

        //[Display(Name = "品牌")]
        //public string Brand { get; set; }
        //[Display(Name = "型号")]
        //public string Model { get; set; }
        //[Display(Name = "规格")]
        //public string Specification { get; set; }
        //[Display(Name = "尺寸")]
        //public string Dimension { get; set; }

        //[Display(Name = "单价")]
        //public string Price { get; set; }
        //[Display(Name = "材料备注")]
        //public string Comments { get; set; }
    
        
    }
}
