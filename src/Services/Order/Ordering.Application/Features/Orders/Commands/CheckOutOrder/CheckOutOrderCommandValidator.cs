using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandValidator : AbstractValidator<CheckOutOrderCommand>
    {
        public CheckOutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than zero.");
        }
    }
}
