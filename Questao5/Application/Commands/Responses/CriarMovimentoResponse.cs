using MediatR;

namespace Questao5.Application.Commands.Responses
{
    public class CriarMovimentoResponse : IRequest
    {
        public Guid IdMovimento { get; set; }
    }
}
