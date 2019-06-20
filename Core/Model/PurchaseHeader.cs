using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Enum;

namespace Core.Model
{
    public class PurchaseHeader : Header
    {
        [Key]
        public string PurchaseNumber { get; set; }

        public string SupplierId { get; set; }
        public RequestCategoriesEnum RequestCategory { get; set; } //需求类别

        public PurchaseCategoryEnum PurchaseCategory { get; set; }
        public DeliveryCategoryEnum DeliveryCategory { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

    }
}
