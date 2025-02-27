﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Abp_BeTech.Models
{
    public class ProductCategorySpecifications : Entity<int>
    {
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("Specification")]
        public int? SpecificationId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual Specification Specification { get; set; }
        public string Value { get; set; }
    }
}
