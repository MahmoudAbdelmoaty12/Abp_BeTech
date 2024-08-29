using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class FiltterProductDto:EntityDto<int>
    {
        public decimal? DiscountValue { get; set; }
        public string? Warranty { get; set; }
        public string? Brand { get; set; }
   
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
      
    }
}
