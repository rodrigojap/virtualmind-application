using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.Interfaces;
using MediatR;
using VirtualMind.Application.Queries;
using VirtualMind.Application.DTOs;
using VirtualMind.Domain.Entities;
using VirtualMind.Domain.Enums;
using System;
using VirtualMind.Application.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VirtualMind.Application.Exceptions;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace VirtualMind.Application.Commands
{
    public class CreateOperationCommand : IRequest<int>
    {
        public int UserId { get; set; }

        public decimal RequestedAmount { get; set; }

        public string CurrencyType { get; set; }
    }

    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, int>
    {
        private readonly IVirtualMindDbContext VirtualMindDbContext;
        private readonly ICurrencyExchangeFactory CurrencyExchangeFactory;
        private readonly ILogger<CreateOperationCommandHandler> Logger;

        public CreateOperationCommandHandler(IVirtualMindDbContext virtualMindDbContext,
                                             ICurrencyExchangeFactory currencyExchangeFactory,
                                             ILogger<CreateOperationCommandHandler> logger)
        {
            VirtualMindDbContext = virtualMindDbContext;
            CurrencyExchangeFactory = currencyExchangeFactory;
            this.Logger = logger;
        }

        public async Task<int> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var currentQuote = await GetCurrentQuote(request.CurrencyType);
            var purchasedAmount = CalcPurchasedAmount(request, currentQuote);
            var currency = (Currency)Enum.Parse(typeof(Currency), request.CurrencyType);

            await CheckLimitOperation(request.UserId, purchasedAmount, currency);

            var operation = new Operation
            {
                Currency = currency,
                UserId = request.UserId,
                RequestedAmount = request.RequestedAmount,
                CurrentQuote = currentQuote.Purchase.ConvertStringToDecimal(),
                PurchasedAmount = purchasedAmount
            };

            await VirtualMindDbContext.Operations.AddAsync(operation);
            return await VirtualMindDbContext.SaveChangesAsync();            
        }

        private async Task<ExchangeRateDTO> GetCurrentQuote(string currencyType)
        {
            var result = await CurrencyExchangeFactory.GetExchangeRate(currencyType);

            var currnetQuote = new ExchangeRateDTO
            {
                Purchase = result[0],
                Sale = result[1],
                LastUpdate = result[2]
            };
            return currnetQuote;
        }

        private decimal CalcPurchasedAmount(CreateOperationCommand request, ExchangeRateDTO currentQuote)
        {
            return decimal.Round(request.RequestedAmount / currentQuote.Purchase.ConvertStringToDecimal(), 2);
        }

        private async Task CheckLimitOperation(int userId, decimal purchasedAmount, Currency currency)
        {
            var userSubmittedOperations = await VirtualMindDbContext.Operations
                                        .Where(o => o.UserId == userId && o.Currency == currency && o.Created.Month == DateTime.Now.Month)
                                        .ToListAsync();

            var currencyParameters = await VirtualMindDbContext.OperationCurrencies.Where(c => c.Currency == currency)
                                                                                   .FirstOrDefaultAsync();

            var total = userSubmittedOperations.Sum(o => o.PurchasedAmount) + purchasedAmount;

            if (total > currencyParameters.Limit)
            {
                Logger.LogInformation($"InvalidOperation[CheckLimitOperation] - user {userId} - at: {DateTime.Now}");

                throw new ValidationException("InvalidOperation", new[] 
                { 
                    $"This operation is not allowed because it exceeds the limits for the currency [{currency}] and user.",
                    $"Limit: [{currencyParameters.Limit}], Operation Total: [{total.ToString(new CultureInfo("en-US"))}]"
                });
            }            
        }
    }
}
