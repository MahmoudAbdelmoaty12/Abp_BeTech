﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class CartItemDto:FullAuditedEntityDto<int>
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int? CopyQuantity { get; set; }
    }
}
