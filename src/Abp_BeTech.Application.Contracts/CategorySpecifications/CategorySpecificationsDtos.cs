using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.CategorySpecifications
{
    public class CategorySpecificationsDtos : EntityDto<int>
    {
      
     public int CategoryId { get; set; }
      public int SpecificationId { get; set; }
    }
}
