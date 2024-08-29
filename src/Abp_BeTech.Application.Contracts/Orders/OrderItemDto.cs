using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Orders
{
    public class OrderItemDto: EntityDto<int>
    {
     
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitePrice { get; set; }
    }
}
