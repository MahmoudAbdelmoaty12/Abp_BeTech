using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class GetAllProductsDtos:FullAuditedEntityDto<int>
    {

        public string Description { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountedPrice {  get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int? Categoryid { get; set; }
        public List<string>? Images { get; set; }
    
        public bool IsDeleted { get; set; }
    }
}
