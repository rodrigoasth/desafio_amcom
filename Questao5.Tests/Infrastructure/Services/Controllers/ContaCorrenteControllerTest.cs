using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Services.Controllers;

namespace Questao5.Tests.Infrastructure.Services.Controllers
{
    public class ContaCorrenteControllerTest
    {
        private ISender _sender;
        private ILogger<ContaCorrenteController> _log;
        private ContaCorrenteController _controller;
        public ContaCorrenteControllerTest()
        {
            _sender = Substitute.For<ISender>();
            _log = Substitute.For<ILogger<ContaCorrenteController>>();
            _controller = new ContaCorrenteController(_log, _sender);
        }

        [Fact]
        public void Retorna_OK_Quando_Request_Criar_Movimento_Eh_Valido()
        {
            //Arrange
            //var substitute = Substitute.For<ISomeInterface>();
            var request = new CriarMovimentoRequest() { 
                TransactionId = Guid.NewGuid(),
                IdConta = Guid.NewGuid(),
                Tipo = Domain.Enumerators.TipoMovimento.Credito,
                Valor = 100
            };

            //Act
            var response = _controller.CriarMovimento(request);

            //Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void Retorna_Bad_Request_Quando_Request_Criar_Movimento_Eh_Nulo()
        {
            //Arrange

            //Act
            var response = _controller.CriarMovimento(null);

            //Assert
            Assert.IsType<BadRequestResult>(response.Result);

        }
    }
}
