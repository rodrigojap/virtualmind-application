using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMind.Application.Extensions;
using VirtualMind.Application.Interfaces;
using VirtuaMind.Infrastructure.RestServices.Template;

namespace VirtuaMind.Infrastructure.RestServices.ExternalServices
{
    public class BancoProvinciaService : RestService, IBancoProvinciaRestService
    {
        private readonly string BASEURL = "https://www.bancoprovincia.com.ar";

        public BancoProvinciaService()
        {
            SetClientRequest(BASEURL);
        }

        public async Task<List<string>> GetUSDExchangeRate()
        {
            var result = await Execute(null, "Principal/Dolar", RestSharp.Method.GET);

            var content = JsonConvert.DeserializeObject<List<string>>(result);

            return content;
        }

        public async Task<List<string>> GetBRLExchangeRate()
        {
            var content = await GetUSDExchangeRate();

            var convertedList = new List<string>() 
            { 
                content[0].ConvertUSDToBRL(),
                content[1].ConvertUSDToBRL(),
                content[2]
            };

            return convertedList;
        }
    }
}
