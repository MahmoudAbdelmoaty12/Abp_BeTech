using Abp_BeTech.CategoryDato;
using Abp_BeTech.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp_BeTech.Categorys
{
    public class CategorySpecificationsDtos
    {
        public CategoryDtos Category { get; set; }
        public List<SpecificationsDto> SpecificationsDtos { get; set; }
    }
}
