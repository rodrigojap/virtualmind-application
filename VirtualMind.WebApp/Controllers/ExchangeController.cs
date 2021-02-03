using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMind.Application.DTOs;
using VirtualMind.Application.Queries;

namespace VirtualMind.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;       

        public ExchangeController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRate([FromQuery]GetCurrencyExchange getCurrencyExchange)
        {
            var response = await this._mediator.Send(getCurrencyExchange);

            return response;
        }
    }
}
