using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp_BeTech.Models
{
    public class Specification : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public ICollection<CategorySpecification> CategorySpecifications { get; set; }
        public Specification()
        {

            CategorySpecifications = new HashSet<CategorySpecification>();

        }
    }
}
