using Abp_BeTech.PtoductDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp_BeTech
{
    public class CreateUpdateProductValidator:AbstractValidator<CreateUpdateProductDtos>
    {
public CreateUpdateProductValidator() {


            RuleFor(x => x.Brand)
              .NotEmpty()
              .MaximumLength(100);
            RuleFor(x => x.Model).MaximumLength(100)
           .NotEmpty()
           .MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.DiscountValue).LessThan(100);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
