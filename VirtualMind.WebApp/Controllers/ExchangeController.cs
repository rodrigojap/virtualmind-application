using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMind.Application.Commands;
using VirtualMind.Application.DTOs;
using VirtualMind.Application.Queries;
using MediatR;

namespace VirtualMind.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {               
        private readonly IMediator _mediator;        

        public ExchangeController(IMediator mediator)
        {            
            _mediator = mediator;            
        }

        [HttpGet]
        public async Task<ExchangeRateDTO> GetExchangeRate([FromQuery]GetCurrencyExchangeQuery getCurrencyExchange)
        {
            return await _mediator.Send(getCurrencyExchange);            
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostExchangeOperation([FromBody]CreateOperationCommand createOperationCommand)
        {
            return await _mediator.Send(createOperationCommand);                     
        }
    }
}
