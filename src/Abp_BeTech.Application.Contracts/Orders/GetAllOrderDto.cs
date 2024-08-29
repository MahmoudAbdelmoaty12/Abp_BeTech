using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Orders
{
    public class GetAllOrderDto:EntityDto<int>
    {
        public int Id { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PayMethod { get; set; }
        public string? OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
