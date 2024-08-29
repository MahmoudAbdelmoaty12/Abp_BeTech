using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Orders
{
    public class GetOrderDetailsDtos : FullAuditedEntity<int>
    {
  
        public int? ProductId { get; set; }//product
        public int? OrderId { get; set; }//order
        public string? Image { get; set; }//product
        public string? Description { get; set; }//product
        public decimal? Price { get; set; }//product
        public int? Quantity { get; set; }//orderitem
    }
}
