using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class PoModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PoNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public ICollection<Contract> Contracts { get; set; }
    }
}
