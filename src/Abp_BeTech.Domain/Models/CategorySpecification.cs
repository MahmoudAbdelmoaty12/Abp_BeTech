using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Abp_BeTech.Models
{
    public class CategorySpecification : Entity<int>
    {
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Specification")]
        public int? SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }
    }
}
