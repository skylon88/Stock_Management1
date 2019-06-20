using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class OutStock
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public double Price { get; set; }
        public double Total { get; set; }
        public string PositionName { get; set; }

        [ForeignKey("OutStockHeader")]
        public string OutStockNumber { get; set; }

        [ForeignKey("Request")]
        public Guid RequestId { get; set; }
        public RequestCategoriesEnum Type { get; set; }
        public ProcessStatusEnum Status { get; set; }

        public virtual Request Request { get; set; }
        public virtual OutStockHeader OutStockHeader { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
