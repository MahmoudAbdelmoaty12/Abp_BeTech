using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Abp_BeTech.Models
{
    public class Product : FullAuditedEntity<int>
    {
        public string Description { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountedPrice => Price - (Price * DiscountValue / 100);
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public ICollection<Image> Images { get; set; }
        [ForeignKey("Category")]
        public int? Categoryid { get; set; }
        public virtual Category Category { get; set; }
     
        public virtual IdentityUser IdentityUser { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
        public ICollection<Review> reviews { get; set; }
        public ICollection<ProductCategorySpecifications> productCategorySpecifications { get; set; }
        public Product()
        {
            productCategorySpecifications = new List<ProductCategorySpecifications>();
            orderItems = new List<OrderItem>();
            Images = new List<Image>();
            reviews= new List<Review>();

        }
    }
}
