using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public int SerialNumber { get; set; } //序号
        public ProcessStatusEnum Status { get; set; } = ProcessStatusEnum.需求建立;
        public string Reason { get; set; } //需求原因
        public string OtherReason { get; set; } //其他需求原因

        public string Note { get; set; } //备注
        public double Total { get; set; }

        public LockStatusEnum LockStatus { get; set; }

        public DateTime? LockTime { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [ForeignKey("FixModel")]
        public Guid? FixModelId { get; set; }

        [ForeignKey("Priority")]
        public int PriorityId { get; set; }
        
        [ForeignKey("RequestHeader")]
        public string RequestNumber { get; set; }
        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual RequestHeader RequestHeader { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual FixModel FixModel { get; set; }

    }
}
