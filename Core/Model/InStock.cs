using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class InStock
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Total { get; set; }
        public string PositionName { get; set; }

        [ForeignKey("InStockHeader")]
        public string InStockNumber { get; set; }

        //[ForeignKey("Purchase")]
        public Guid? PurchaseId { get; set; }
        public Guid? RequestId { get; set; }
        public RequestCategoriesEnum Type { get; set; }
        public ProcessStatusEnum Status { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public virtual InStockHeader InStockHeader { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }


}
