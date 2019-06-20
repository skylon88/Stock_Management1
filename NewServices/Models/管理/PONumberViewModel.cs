using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NewServices.Models.需求部分;

namespace NewServices.Models.管理
{
    public class PoNumberViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "PO编号")]
        public string PoNumber { get; set; }
        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "更新时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }
        public BindingList<ContractViewModel> Contracts { get; set; }
    }

    public class ContractViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "合同编号")]
        public string ContractNumber { get; set; }
        [Display(Name = "合同地址")]
        public string Address { get; set; }
        [Display(Name = "创建时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "更新时间"), DisplayFormat(DataFormatString = "yyyy-MM-dd hh:mm tt")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "相关需求")]
        public BindingList<RequestHeaderViewModel> RequestHeaderViewModels { get; set; }
    }
}
