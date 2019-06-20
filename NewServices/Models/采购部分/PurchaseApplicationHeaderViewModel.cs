using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.采购部分
{
    public class PurchaseApplicationHeaderViewModel
    {
        [Display(Name = "申请类别")]
        public RequestCategoriesEnum RequestCategory { get; set; }
        [Display(Name = "采购申请编号")]
        public string ApplicationNumber { get; set; }
        [Display(Name = "需求编号")]
        public string RequestHeaderNumber { get; set; }
        [Display(Name = "PO编号")]
        public string PoNumber { get; set; }
        [Display(Name = "申请人")]
        public string ApplicationPerson { get; set; } //需求申请人
        [Display(Name = "制表人")]
        public string CreatePerson { get; set; } //制表人
        [Display(Name = "物管跟进人")]
        public string FollowupPerson { get; set; } //物管跟进人
        [Display(Name = "审核人")]
        public string Auditor { get; set; } //审核人
        [Display(Name = "审核部")]
        public string AuditDepart { get; set; } //审核部
        [Display(Name = "申请总数")]
        public int TotalApplied { get; set; }
        [Display(Name = "审批总数")]
        public int TotalConfirmed { get; set; }
        [Display(Name = "审批状态"), EnumDataType(typeof(AuditStatusEnum))]
        public AuditStatusEnum AuditStatus { get; set; }

        [Display(Name = "状态")]
        public ProcessStatusEnum Status { get; set; }

        [Display(Name = "完成率")]
        public decimal CompletePercentage { get; set; }
        public BindingList<PurchaseApplicationViewModel> PurchaseApplicationViewModels { get; set; }

        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }
    }
}
