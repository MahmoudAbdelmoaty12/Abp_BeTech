using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Abp_BeTech.Orders
{
    public class GetAllOrderItemDto:Entity<int>
    {
  
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
 
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
