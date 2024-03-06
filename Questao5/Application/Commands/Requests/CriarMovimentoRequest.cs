using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CriarMovimentoRequest : IRequest<CriarMovimentoResponse>
    {
        public Guid TransactionId { get; set; }
        public Guid IdConta { get; set; }
        public double Valor { get; set; }
        public TipoMovimento Tipo { get; set; }
    }
}
