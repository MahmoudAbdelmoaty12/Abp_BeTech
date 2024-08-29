
using Abp_BeTech.PtoductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.Ptoduct
{
    public class ProductCategorySpecificationsListDto:EntityDto<int>
    {
        public CreateUpdateProductDtos CreateUpdateProductDtos { get; set; }
        public List<ProductCategorySpecificatoinDto> ProductCategorySpecifications { get; set; }
    }
}
