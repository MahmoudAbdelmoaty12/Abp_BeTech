using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Abp_BeTech.Models
{
    public class Review : FullAuditedEntity<int>
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public virtual Product Product { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}