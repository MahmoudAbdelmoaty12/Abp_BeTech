using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Orders
{
    public class GitListOrderDtos:FullAuditedEntity<int>
    {
     
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PayMethod { get; set; }
        public string? OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
