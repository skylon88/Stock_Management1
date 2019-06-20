using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class PurchaseApplication
    {
        public Guid PurchaseApplicationId { get; set; } = Guid.NewGuid();
        public int SerialNumber { get; set; }
        public double CurrentPurchasePrice { get; set; }
        public double TotalApplied { get; set; }
        public double TotalConfirmed { get; set; }
        public string SupplierId { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }
        public Guid RequestId { get; set; }
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey("PurchaseApplicationHeader")]
        public string PurchaseApplicationNumber { get; set; }

        public virtual PurchaseApplicationHeader PurchaseApplicationHeader { get; set; }
        [Display(Name = "需求等级"), EnumDataType(typeof(PriorityEnum))]
        public PriorityEnum Priority { get; set; }
        [Display(Name = "审批状态"), EnumDataType(typeof(AuditStatusEnum))]
        public AuditStatusEnum AuditStatus { get; set; }
        [Display(Name = "状态"), EnumDataType(typeof(ProcessStatusEnum))]
        public ProcessStatusEnum ProcessStatus { get; set; }

        public string SelectedPurchaseNumber { get; set; } //Only for 退货申请
        public string SelectedPONumber { get; set; } //Only for 退货申请
    }
}
