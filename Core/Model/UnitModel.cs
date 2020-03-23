using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class UnitModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ItemName { get; set; }

        public string DefaultUnit { get; set; }
        public string ConvertToUnit { get; set; }

        public double Factor { get; set; }
        public bool IsGeneral { get; set; }
    }
}
