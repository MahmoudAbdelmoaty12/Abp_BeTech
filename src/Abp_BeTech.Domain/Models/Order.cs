using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Abp_BeTech.Models
{
    public enum OrderStatus
    {
        Pending = 0,
        Shipped = 1,
        Delivered = 2,
    }
    public class Order : FullAuditedEntity<int>
    {
        public decimal TotalPrice { get; set; }
        public int? Phone { get; set; }
        public DateTime OderDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; }
        public DateTime? DeliveryDate { get; set; } = DateTime.Now.AddDays(3);
        public string ShippingAddress { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual IdentityUser IdentityUser { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public Order()
        {

            orderItems = new List<OrderItem>();
        }

    }
}
