
using Abp_BeTech.Ptoduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class ProductCategorySpecificationsListDtos:EntityDto<int>
    {
        public CreateUpdateProductDtos CreateUpdateProductDtos { get; set; }
        public List<ProductCategorySpecificatoinDto> ProductCategorySpecifications { get; set; }
    }
}
