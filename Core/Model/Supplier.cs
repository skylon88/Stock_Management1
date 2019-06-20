using System;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace Core.Model
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Code { get; set; }

        public bool IsActive { get; set; }

        public SupplierType Type { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
