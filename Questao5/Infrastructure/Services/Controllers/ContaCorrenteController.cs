using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly ISender _sender;

        public ContaCorrenteController(ILogger<ContaCorrenteController> logger, ISender sender)
        {
            this._logger = logger;
            this._sender = sender;
        }

        [HttpGet(Name = "ConsultarSaldo")]
        public IActionResult ConsultarSaldo(int numeroConta)
        {
            try
            {
                var request = new ConsultarSaldoRequest { NumeroConta = numeroConta };
                var result = _sender.Send(request);

                return Ok(result.Result);
            }
            catch(AggregateException ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost(Name ="CriarMovimento")]
        public async Task<IActionResult> CriarMovimento(CriarMovimentoRequest request)
        {
            try
            {
                if (request == null) return BadRequest();

                return Ok(request?.TransactionId);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
