using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace NewServices.Models.需求部分
{
    public class RequestHeaderViewModel
    {
        [Display(Name = "需求编号")]
        public string RequestHeaderNumber { get; set; }
        [Display(Name = "需求类别"), EnumDataType(typeof(RequestCategoriesEnum))]
        public RequestCategoriesEnum RequestCategory { get; set; }//需求类别
        [Display(Name = "PO编号")]
        public string PoNumber { get; set; }
        [Display(Name = "合同编号")]
        public string ContractId { get; set; }
        [Display(Name = "合同地址")]
        public string ContractAddress { get; set; }

        [Display(Name = "需求申请人")]
        public string ApplicationPerson { get; set; } //需求申请人
        [Display(Name = "制表人")]
        public string CreatePerson { get; set; } //制表人
        [Display(Name = "物管跟进人")]
        public string FollowupPerson { get; set; } //物管跟进人
        [Display(Name = "审核人")]
        public string Auditor { get; set; } //审核人
        [Display(Name = "审核部")]
        public string AuditDepart { get; set; } //审核部

        [Display(Name = "分配锁定")]
        public LockStatusEnum LockStatus { get; set; }

        [Display(Name = "数量")]
        public int Total { get; set; } //共多少个请求项目
        [Display(Name = "状态")]
        public ProcessStatusEnum Status { get; set; }

        [Display(Name = "完成率")]
        public decimal CompletePercentage { get; set; }

        public BindingList<RequestViewModel> RequestViewModels { get; set; }

        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }
    }
}
