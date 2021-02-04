﻿using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.DTOs;
using MediatR;

namespace VirtualMind.Application.Queries
{
    public class GetCurrencyExchangeQuery : IRequest<ExchangeRateDTO>
    {        
        public string CurrencyType { get; set; }        
    }    

    public class GetCurrencyExchangeHandler : IRequestHandler<GetCurrencyExchangeQuery, ExchangeRateDTO>
    {
        private readonly ICurrencyExchangeFactory CurrencyExchangeFactory;

        public GetCurrencyExchangeHandler(ICurrencyExchangeFactory currencyExchangeFactory)
        {
            CurrencyExchangeFactory = currencyExchangeFactory;
        }

        public async Task<ExchangeRateDTO> Handle(GetCurrencyExchangeQuery request, CancellationToken cancellationToken)
        {
            var result = await CurrencyExchangeFactory.GetExchangeRate(request.CurrencyType);

            var exchangeList = new ExchangeRateDTO
            {
                Purchase = result[0],
                Sale = result[1],
                LastUpdate = result[2]
            };

            return exchangeList;
        }
    }
}
