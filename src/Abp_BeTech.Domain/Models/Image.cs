using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Abp_BeTech.Models
{
    public class Image : FullAuditedEntity<int>
    {
        [Base64String]
        public string Name { get; set; }

        public virtual Product Product { get; set; }

      
    }
}
