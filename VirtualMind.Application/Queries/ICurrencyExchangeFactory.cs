using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtualMind.Application.Queries
{
    public interface ICurrencyExchangeFactory
    {
        Task<List<string>> GetExchangeRate(string currency);
    }
}
