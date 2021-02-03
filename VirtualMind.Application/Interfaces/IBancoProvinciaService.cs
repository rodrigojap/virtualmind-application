using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtualMind.Application.Interfaces
{
    public interface IBancoProvinciaRestService
    {
        Task<List<string>> GetUSDExchangeRate();

        Task<List<string>> GetBRLExchangeRate();
    }
}
