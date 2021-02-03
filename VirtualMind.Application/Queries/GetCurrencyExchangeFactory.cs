using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMind.Application.Interfaces;
using VirtualMind.Domain.Enums;

namespace VirtualMind.Application.Queries
{
    public class GetCurrencyExchangeFactory : ICurrencyExchangeFactory
    {
        private readonly IBancoProvinciaRestService BancoProvinciaRestService;

        public GetCurrencyExchangeFactory(IBancoProvinciaRestService bancoProvinciaRestService)
        {
            BancoProvinciaRestService = bancoProvinciaRestService;
        }

        public async Task<List<string>> GetExchangeRate(string currency)
        {
            var currencyType = (Currency)Enum.Parse(typeof(Currency), currency);

            switch (currencyType)
            {
                case Currency.USD:
                    return await BancoProvinciaRestService.GetUSDExchangeRate();
                case Currency.BRL:
                    return await BancoProvinciaRestService.GetBRLExchangeRate();
                default:
                    throw new Exception("Uops, this currency is not allowed!");
            }
        }
    }
}
