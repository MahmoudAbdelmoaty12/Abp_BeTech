using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class CreateUpdateProductDtos:FullAuditedEntityDto<int>
    {
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountedPrice => Price * DiscountValue / 100;
        public string? Warranty { get; set; }
        public int? Quantity { get; set; }
        public int? Categoryid { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
