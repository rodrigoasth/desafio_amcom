using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Exceptions;

namespace Questao5.Application.Handlers
{
    public class CriarMovimentoHandler : IRequestHandler<CriarMovimentoRequest, CriarMovimentoResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public CriarMovimentoHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }
        public async Task<CriarMovimentoResponse> Handle(CriarMovimentoRequest request, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteRepository.ObterConta(request.IdConta);

            if (conta == null) throw new ContaInvalidaException();

            var movimento = conta.CriarMovimento(request.Valor, request.Tipo);

            var idMovimento = _contaCorrenteRepository.AdicionarMovimentoAsync(movimento);

            var response = new CriarMovimentoResponse() { IdMovimento = idMovimento };

            return response;
        }
    }
}
