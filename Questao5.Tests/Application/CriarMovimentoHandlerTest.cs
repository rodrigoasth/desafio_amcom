using AutoBogus;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Exceptions;

namespace Questao5.Tests.Application
{
    public class CriarMovimentoHandlerTest
    {
        CriarMovimentoHandler _handler;
        IContaCorrenteRepository _contaRepository;

        public CriarMovimentoHandlerTest()
        {
            _contaRepository = Substitute.For<IContaCorrenteRepository>();  
            _handler = new CriarMovimentoHandler(_contaRepository);
        }

        [Fact]
        public void Retorna_Id_Movimento_Quando_Command_Criar_Movimento_Eh_Valido()
        {
            //Arrange
            var request = new CriarMovimentoRequest() { };
            var conta = AutoFaker.Generate<ContaCorrente>();
            _contaRepository.ObterConta(Arg.Any<Guid>()).Returns(conta);
            _contaRepository.AdicionarMovimentoAsync(Arg.Any<Movimento>()).Returns(Guid.NewGuid());

            //Act
            var result = _handler.Handle(request, CancellationToken.None);

            //Assert
            _contaRepository.Received().ObterConta(Arg.Any<Guid>());
            _contaRepository.Received().AdicionarMovimentoAsync(Arg.Any<Movimento>());
            Assert.NotNull(result.Result);
            Assert.IsType<CriarMovimentoResponse>(result.Result);
        }

        [Fact]
        public async void Retorna_Erro_Conta_Invalida_Quando_Command_Criar_Movimento_Contem_Conta_Nao_Cadastrada()
        {
            //Arrange
            var request = new CriarMovimentoRequest() { };
            var conta = AutoFaker.Generate<ContaCorrente>();
            _contaRepository.ObterConta(Arg.Any<Guid>()).ReturnsNull();


            //Act
           var exception= await Assert.ThrowsAsync<ContaInvalidaException>(() => _handler.Handle(request, CancellationToken.None));
           Assert.IsType<ContaInvalidaException>(exception);
        }

        [Fact]
        public async void Retorna_Erro_Conta_Inativa_Quando_Command_Criar_Movimento_Contem_Conta_Inativa()
        {
            //Arrange
            var request = new CriarMovimentoRequest() { };
            var conta = AutoFaker.Generate<ContaCorrente>();
            conta.Ativo = false;
            _contaRepository.ObterConta(Arg.Any<Guid>()).Returns(conta);

            //Act
            var exception = await Assert.ThrowsAsync<ContaInativaException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.IsType<ContaInativaException>(exception);
        }

        [Fact]
        public async void Retorna_Erro_Valor_Invalido_Quando_Command_Criar_Movimento_Contem_Valor_Negativo()
        {
            //Arrange
            var request = new CriarMovimentoRequest() { Valor = -100 };
            var conta = AutoFaker.Generate<ContaCorrente>();
            conta.Ativo = true;
            _contaRepository.ObterConta(Arg.Any<Guid>()).Returns(conta);

            //Act
            var exception = await Assert.ThrowsAsync<ValorInvalidoException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.IsType<ValorInvalidoException>(exception);
        }

    }
}
