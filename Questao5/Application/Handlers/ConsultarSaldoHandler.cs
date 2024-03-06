using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Exceptions;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoRequest, ConsultarSaldoResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private const string ERRO_CONTA_INVALIDA = "Conta inválida";

        public ConsultarSaldoHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ConsultarSaldoResponse> Handle(ConsultarSaldoRequest request, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteRepository.ConsultarSaldoAsync(request.NumeroConta);

            if (conta == null) throw new ContaInvalidaException();

            var response = new ConsultarSaldoResponse();

            response.NumeroConta = conta.NumeroConta;
            response.NomeTitular = conta.NomeTitular;
            response.Saldo = conta.CalcularSaldo();

            return response;
        }
    }
}
