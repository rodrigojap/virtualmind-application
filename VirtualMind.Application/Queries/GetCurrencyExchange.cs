using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.DTOs;

namespace VirtualMind.Application.Queries
{
    public class GetCurrencyExchange : IRequest<List<ExchangeRateDTO>>
    {
        //public AcceptableCurrencies CurrencyType { get; set; }        

        public string CurrencyType { get; set; }        
    }

    //TODO: Put this on domain
    public enum AcceptableCurrencies
    {
        USD,
        REAL
    }

    public class GetCurrencyExchangeHandler : IRequestHandler<GetCurrencyExchange, List<ExchangeRateDTO>>
    {
        public async Task<List<ExchangeRateDTO>> Handle(GetCurrencyExchange request, CancellationToken cancellationToken)
        {
            var exchange = new ExchangeRateDTO();
            exchange.LastUpdate = "today";
            exchange.Purchase = "12.00";
            exchange.Sale = "10";

            var exchangeList = new List<ExchangeRateDTO>();
            exchangeList.Add(exchange);

            return await Task.FromResult(exchangeList);
        }
    }
}
