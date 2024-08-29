using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Abp_BeTech.Orders
{
    public class GetOrderWithItemsDto:Entity<int>
    {
        public OrderWithoutItemsDto order { get; set; }
        public List<GetOrderDetailsDtos> Details { get; set; }
    }
}
