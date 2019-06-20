using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class PurchaseApplicationHeader :Header
    {
        [Key]
        public string PurchaseApplicationNumber { get; set; }

        [ForeignKey("RequestHeader")]
        public string RequestNumber { get; set; }

        public RequestCategoriesEnum RequestCategory { get; set; }
        public RequestHeader RequestHeader { get; set; }
        public ICollection<PurchaseApplication> PurchaseApplications { get; set; }
    }
}
