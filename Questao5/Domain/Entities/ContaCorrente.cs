using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente : IAggregateRoot
    {
        public Guid Id { get; set; }
        public List<Movimento> Movimentos { get; } 
        public int NumeroConta { get; set; }
        public string NomeTitular { get; set; }
        public bool Ativo { get; set; }

        public ContaCorrente()
        {
            Movimentos = new List<Movimento>();
        }

        public double CalcularSaldo()
        {
            if (!Ativo) throw new ContaInativaException();

            return 0;
        }

        public Movimento CriarMovimento(double valor, TipoMovimento tipo)
        {
            if (!Ativo) throw new ContaInativaException();
            if(valor < 0) throw new ValorInvalidoException();

            Movimento movimento = new Movimento(Id, valor, tipo);

            Movimentos.Add(movimento);

            return movimento;            
        }
    }
}
