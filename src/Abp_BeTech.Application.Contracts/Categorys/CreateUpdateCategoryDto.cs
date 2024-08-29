using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.Categorys
{
    public class CreateUpdateCategoryDto:EntityDto<int>
    {
       
        public string Name { get; set; }
    }
}
