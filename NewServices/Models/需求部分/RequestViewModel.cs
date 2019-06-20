using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;
using NewServices.Models.管理;

namespace NewServices.Models.需求部分
{
    public class RequestViewModel
    {
        //From Request table
        public Guid RequestId { get; set; }

        [Display(Name = "序号")]
        public int SerialNumber { get; set; }

        [Display(Name = "需求编号")]
        public string RequestNumber { get; set; }

        [Display(Name = "需求类别"), EnumDataType(typeof(RequestCategoriesEnum))]
        public RequestCategoriesEnum RequestCategory { get; set; }
        public Guid ItemId { get; set; }
        [Display(Name = "物品名称")]
        public string Name { get; set; }
        [Display(Name = "物品编号")]
        public string Code { get; set; }
        [Display(Name = "需求数量")]
        public double Total { get; set; }
        [Display(Name = "库位")]
        public string PositionName { get; set; }
        [Display(Name = "库位数量")]
        public double TotalInStorage { get; set; }
        public double Max { get; set; }
        [Display(Name = "可用库存数量")]
        public double AvailableInStorage { get; set; }
        [Display(Name = "可采购")]
        public double ToApplyTotal { get; set; }
        [Display(Name = "可入库")]
        public double ToInStockTotal { get; set; } = 0;
        [Display(Name = "已入库")]
        public double InStockTotal { get; set; } = 0;
        [Display(Name = "可出库")]
        public double ToOutStockTotal { get; set; } = 0;
        [Display(Name = "已出库")]
        public double OutStockTotal { get; set; } = 0;
        [Display(Name = "可报废")]
        public double ToDestoryTotal { get; set; } = 0;       
        [Display(Name = "已报废")]
        public double DestoriedTotal { get; set; } = 0;
        [Display(Name = "维修地址")]
        public string FixAddress { get; set; } //维修地址
        [Display(Name = "联系人")]
        public string Contact { get; set; } //维修联系人
        [Display(Name = "联系电话")]
        public string Phone { get; set; } //维修联系人电话
        [Display(Name = "维修金额"), DisplayFormat(DataFormatString = "{0:C0}")]
        public double FixingPrice { get; set; } //维修金额
        [Display(Name = "维修天数")]
        public int FixingDays { get; set; } //维修天数
        [Display(Name = "取货日期")]
        public DateTime? FixingFinishDate { get; set; } //取货日期
        [Display(Name = "单位")]
        public string Unit { get; set; }
        [Display(Name = "需求等级"),EnumDataType(typeof(PriorityEnum))]
        public PriorityEnum Priority { get; set; }
        [Display(Name = "需求原因")]
        public string Reason { get; set; } 
        [Display(Name = "其他需求原因")]
        public string OtherReason { get; set; }

        [Display(Name = "分配锁定")]
        public LockStatusEnum LockStatus { get; set; }

        [Display(Name = "锁定时间"), DisplayFormat(DataFormatString = "yy-MM-dd hh:mm")]
        public DateTime? LockTime { get; set; }

        [Display(Name = "跟进状态")]
        public ProcessStatusEnum Status { get; set; }

       
        [Display(Name = "备注")]
        public string Note { get; set; }

        public bool ToDestroy { get; set; } //报废操作 

        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }
        public string PoNumber { get; set; }

        public BindingList<PositionViewModel> PositionViewModels { get; set; }
    }
}
