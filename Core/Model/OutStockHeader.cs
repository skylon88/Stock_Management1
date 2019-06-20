using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class OutStockHeader :Header
    {
        [Key]
        public string OutStockHeaderNumber { get; set; }
        [ForeignKey("RequestHeader")]
        public string RequestNumber { get; set; }
        public string StorageNumber { get; set; }
        public ICollection<OutStock> OutStocks { get; set; }
        public virtual RequestHeader RequestHeader { get; set; }
    }
}
