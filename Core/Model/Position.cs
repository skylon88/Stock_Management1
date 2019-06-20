using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string PositionName { get; set; }
        public double Total { get; set; }

        public string Area { get; set; }

        public string Location { get; set; }
        public string Comment { get; set; }

        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string LatestInStockNumber { get; set; }

        [ForeignKey("Item")]
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
