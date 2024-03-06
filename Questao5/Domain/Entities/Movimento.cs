using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Guid Id { get; set; }
        public Guid IdContaCorrente { get; init; }
        public DateTime Data { get; }
        public TipoMovimento Tipo { get; init; }
        public double Valor { get; init; }

        public Movimento(Guid idContaCorrente, double valor, TipoMovimento tipo)
        {
            IdContaCorrente = idContaCorrente;
            Data = DateTime.Now;
            Valor = valor;
            Tipo = tipo;
        }
    }
}
