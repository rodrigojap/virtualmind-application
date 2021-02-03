using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualMind.Application.DTOs;
using VirtualMind.Application.Interfaces;
using VirtualMind.Application.Queries;

namespace VirtualMind.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {       
        private readonly ILogger<ExchangeController> _logger;
        private readonly IMediator _mediator;
        private readonly IVirtualMindDbContext _virtualMindDbContext;

        public ExchangeController(ILogger<ExchangeController> logger, 
                                  IMediator mediator,
                                  IVirtualMindDbContext virtualMindDbContext)
        {
            _logger = logger;
            _mediator = mediator;
            _virtualMindDbContext = virtualMindDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRate([FromQuery]GetCurrencyExchange getCurrencyExchange)
        {
            var response = await this._mediator.Send(getCurrencyExchange);

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> PostExchangeOperation()
        {                                    
            //await _virtualMindDbContext.Operations.AddAsync(new Domain.Entities.Operation
            //{
            //    Currency = Domain.Enums.Currency.BRL,
            //    CurrentQuote = 10,
            //    PurchasedAmount = 15,
            //    RequestedAmount = 15,
            //    UserId = 1               
            //});

            //await _virtualMindDbContext.SaveChangesAsync();

            //var list = await _virtualMindDbContext.Operations.AsNoTracking().ToListAsync();

            return Ok();
        }
    }
}
