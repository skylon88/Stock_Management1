using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class Contract
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ContractNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [ForeignKey("PoModel")]
        public Guid? PoId { get; set; }
        public virtual PoModel PoModel { get; set; }

        public ICollection<RequestHeader> RequestHeaders { get; set; }
    }
}
