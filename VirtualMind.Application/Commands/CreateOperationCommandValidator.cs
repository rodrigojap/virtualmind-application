using FluentValidation;
using VirtualMind.Domain.Enums;

namespace VirtualMind.Application.Commands
{
    public class CreateOperationCommandValidator : AbstractValidator<CreateOperationCommand>
    {
        public CreateOperationCommandValidator()
        {
            RuleFor(input => input.UserId)
                .GreaterThan(0).WithMessage("[UserId] must be greater Than 0")
                .NotNull().WithMessage("[UserId] can't be null!");

            RuleFor(input => input.RequestedAmount)
                .GreaterThan(0).WithMessage("[RequestedAmount] must be greater Than 0")
                .NotNull().WithMessage("[RequestedAmount] can't be null!");

            RuleFor(input => input.CurrencyType)
                .NotNull().WithMessage("[CurrencyType] can't be null!")
                .NotEmpty().WithMessage("[CurrencyType] field is required!")
                .IsEnumName(typeof(Currency), false).WithMessage("Invalid [CurrencyType]!");
        }
    }
}
