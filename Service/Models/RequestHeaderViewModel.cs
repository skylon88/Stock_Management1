using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Models
{
    public class RequestHeaderViewModel
    {
        [Display(Name = "需求编号")]
        public string RequestHeaderNumber { get; set; }

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
        [Display(Name = "数量")]
        public int Total { get; set; } //共多少个请求项目
        [Display(Name = "状态")]
        public string Status { get; set; }

        public BindingList<RequestViewModel> RequestViewModels { get; set; }

        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "修改时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
