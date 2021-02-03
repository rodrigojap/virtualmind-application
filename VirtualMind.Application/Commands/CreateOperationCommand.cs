using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.Interfaces;
using MediatR;
using VirtualMind.Application.Queries;
using VirtualMind.Application.DTOs;
using System.Collections.Generic;
using VirtualMind.Domain.Entities;
using VirtualMind.Domain.Enums;
using System;
using VirtualMind.Application.Extensions;

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

        public CreateOperationCommandHandler(IVirtualMindDbContext virtualMindDbContext, ICurrencyExchangeFactory currencyExchangeFactory)
        {
            VirtualMindDbContext = virtualMindDbContext;
            CurrencyExchangeFactory = currencyExchangeFactory;
        }

        public async Task<int> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var currentQuote = await GetCurrentQuote(request.CurrencyType);            
            var purchasedAmount = CalcPurchasedAmount(request, currentQuote);


            


            var operation = new Operation
            {
                Currency = (Currency)Enum.Parse(typeof(Currency), request.CurrencyType),
                UserId = request.UserId,
                RequestedAmount = request.RequestedAmount,
                CurrentQuote = currentQuote.Purchase.ConvertStringToDecimal(),
                PurchasedAmount = purchasedAmount
            };

            await VirtualMindDbContext.Operations.AddAsync(operation);
            await VirtualMindDbContext.SaveChangesAsync();

            return await Task.FromResult(0);
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

        private static decimal CalcPurchasedAmount(CreateOperationCommand request, ExchangeRateDTO currentQuote)
        {
            return decimal.Round(request.RequestedAmount / currentQuote.Purchase.ConvertStringToDecimal(), 2);
        }        
    }
}
