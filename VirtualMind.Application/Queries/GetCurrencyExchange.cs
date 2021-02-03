﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.DTOs;
using VirtualMind.Application.Interfaces;

namespace VirtualMind.Application.Queries
{
    public class GetCurrencyExchange : IRequest<List<ExchangeRateDTO>>
    {        
        public string CurrencyType { get; set; }        
    }    

    public class GetCurrencyExchangeHandler : IRequestHandler<GetCurrencyExchange, List<ExchangeRateDTO>>
    {
        private readonly ICurrencyExchangeFactory CurrencyExchangeFactory;

        public GetCurrencyExchangeHandler(ICurrencyExchangeFactory currencyExchangeFactory)
        {
            CurrencyExchangeFactory = currencyExchangeFactory;
        }

        public async Task<List<ExchangeRateDTO>> Handle(GetCurrencyExchange request, CancellationToken cancellationToken)
        {
            var result = await CurrencyExchangeFactory.GetExchangeRate(request.CurrencyType);

            var exchangeList = new List<ExchangeRateDTO>
            {
                new ExchangeRateDTO
                {
                    Purchase = result[0],
                    Sale = result[1],
                    LastUpdate = result[2]
                }
            };            

            return exchangeList;
        }
    }
}
