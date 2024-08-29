
using Abp_BeTech.Ptoduct;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class ProductWithSpecificationsDto:EntityDto<int>
    {
    
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public string? Warranty { get; set; }
        public int? Quantity { get; set; }
        public int? Categoryid { get; set; }
        public List<IFormFile>? Images { get; set; }

    public List<ProductCategorySpecificatoinDto> productCategorySpecificatoinDto { get; set; }
        public ProductWithSpecificationsDto()
        {
           Images = new List<IFormFile>();
       productCategorySpecificatoinDto= new List<ProductCategorySpecificatoinDto>();
        }
    }
}
