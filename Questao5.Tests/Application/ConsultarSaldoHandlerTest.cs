using AutoBogus;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Exceptions;

namespace Questao5.Tests.Application
{
    public class ConsultarSaldoHandlerTest
    {
        ConsultarSaldoHandler _handler;
        IContaCorrenteRepository _contaRepository;

        public ConsultarSaldoHandlerTest()
        {
            _contaRepository = Substitute.For<IContaCorrenteRepository>();
            _handler = new ConsultarSaldoHandler(_contaRepository);
        }

        [Fact]
        public void Retorna_Id_Movimento_Quando_Command_Consultar_Saldo_Eh_Valido()
        {
            //Arrange
            var request = AutoFaker.Generate<ConsultarSaldoRequest>();
            var conta = AutoFaker.Generate<ContaCorrente>();
            conta.Ativo = true;
            _contaRepository.ConsultarSaldoAsync(Arg.Any<int>()).Returns(conta);

            //Act
            var result = _handler.Handle(request, CancellationToken.None);

            //Assert
            _contaRepository.Received().ConsultarSaldoAsync(Arg.Any<int>());
            Assert.NotNull(result.Result);
            Assert.IsType<ConsultarSaldoResponse>(result.Result);
        }

        [Fact]
        public async void Retorna_Erro_Conta_Invalida_Quando_Command_Consultar_Saldo_Contem_Conta_Nao_Cadastrada()
        {
            //Arrange
            var request = AutoFaker.Generate<ConsultarSaldoRequest>(); ;
            var conta = AutoFaker.Generate<ContaCorrente>();
            _contaRepository.ConsultarSaldoAsync(Arg.Any<int>()).ReturnsNull();


            //Act
            var exception = await Assert.ThrowsAsync<ContaInvalidaException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.IsType<ContaInvalidaException>(exception);
        }

        [Fact]
        public async void Retorna_Erro_Conta_Inativa_Quando_Command_Consultar_Saldo_Contem_Conta_Inativa()
        {
            //Arrange
            var request = AutoFaker.Generate<ConsultarSaldoRequest>();
            var conta = AutoFaker.Generate<ContaCorrente>();
            conta.Ativo = false;
            _contaRepository.ConsultarSaldoAsync(Arg.Any<int>()).Returns(conta);

            //Act
            var exception = await Assert.ThrowsAsync<ContaInativaException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.IsType<ContaInativaException>(exception);
        }
    }
}
