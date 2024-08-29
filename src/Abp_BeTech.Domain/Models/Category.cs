using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Models
{
    public class Category : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<CategorySpecification> CategorySpecifications { get; set; }
        public ICollection<ProductCategorySpecifications> ProductCategorySpecifications { get; set; }
        public Category()
        {
            Products = new List<Product>();
            CategorySpecifications = new List<CategorySpecification>();
            ProductCategorySpecifications = new List<ProductCategorySpecifications>();
        }
    }
}
