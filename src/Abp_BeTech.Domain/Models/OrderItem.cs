using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Models
{
    public class OrderItem :Entity<int>
    {
      
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice => Quantity * UnitPrice;
       
        public virtual Order Order { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual Product Product { get; set; }
    }
}
