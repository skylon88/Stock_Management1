using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class InStockHeader :Header
    {
        [Key]
        public string InStockNumber { get; set; }
        public string StorageNumber { get; set; } = "Stage";
        public string PurchaseNumber { get; set; }
        public string RequestNumber { get; set; }
        public ICollection<InStock> InStocks { get; set; }
    }

}
