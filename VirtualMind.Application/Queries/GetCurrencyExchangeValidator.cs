using FluentValidation;

namespace VirtualMind.Application.Queries
{
    public class GetCurrencyExchangeValidator : AbstractValidator<GetCurrencyExchange>
    {
        public GetCurrencyExchangeValidator()
        {
            RuleFor(input => input.CurrencyType)
                .NotNull().WithMessage("[CurrencyType] can't be null!")
                .NotEmpty().WithMessage("[CurrencyType] field is required!");           
        }
    }
}
