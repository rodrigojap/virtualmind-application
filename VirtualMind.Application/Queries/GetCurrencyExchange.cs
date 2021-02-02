using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VirtualMind.Application.DTOs;

namespace VirtualMind.Application.Queries
{
    public class GetCurrencyExchange : IRequest<List<ExchangeRateDTO>>
    {        
        public string CurrencyType { get; set; }        
    }    

    public class GetCurrencyExchangeHandler : IRequestHandler<GetCurrencyExchange, List<ExchangeRateDTO>>
    {
        public async Task<List<ExchangeRateDTO>> Handle(GetCurrencyExchange request, CancellationToken cancellationToken)
        {                                    
            //MUST BE A SERVICE API
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
