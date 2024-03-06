using MediatR;

namespace Questao5.Application.Queries.Responses
{
    public class ConsultarSaldoResponse : IRequest
    {
        public int NumeroConta { get; set; }
        public string NomeTitular { get; set; }
        public DateTime DataConsulta => DateTime.Now;
        public double Saldo { get; set; }
    }
}
