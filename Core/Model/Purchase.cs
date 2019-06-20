using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class Purchase
    {
        public Guid PurchaseId { get; set; } = Guid.NewGuid();
        public double CurrentPurchasePrice { get; set; }

        public bool IsPriceChange { get; set; }
        public double PurchaseTotal { get; set; }
        public double ReadyForInStock { get; set; }

        [Display(Name = "状态"), EnumDataType(typeof(ProcessStatusEnum))]
        public ProcessStatusEnum Status { get; set; }  //Only for  采购中 = 3, //采购完成 = 4,
        public string Note { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public DateTime? DeliveryDate { get; set; } 

        public DateTime? LastDeliveryDate { get; set; }
        public double CorrectionTotal { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("PurchaseHeader")]
        public string PurchaseNumber { get; set; }
        public Guid PurchaseApplicationId { get; set; }
        public virtual PurchaseApplication PurchaseApplication { get;set;}
        public virtual PurchaseHeader PurchaseHeader { get; set; }
        //public ICollection<InStock> InStocks { get; set; }
    }
}
