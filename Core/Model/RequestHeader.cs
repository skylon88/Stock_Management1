using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum;

namespace Core.Model
{
    public class RequestHeader :Header
    {
        [Key]
        public string RequestHeaderNumber { get; set; }

        //-------------Relateionship--------------------
        [ForeignKey("Contract")]
        public Guid ContractId { get; set; }

        public RequestCategoriesEnum RequestCategory { get; set; } //需求类别

        public virtual Contract Contract { get; set; }

        public ICollection<Request> Requests { get; set; }

        public ICollection<OutStockHeader> OutStockHeaders { get; set; }

    }
 }
