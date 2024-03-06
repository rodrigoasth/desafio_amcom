using System.Runtime.Serialization;

namespace Questao5.Domain.Exceptions
{
    [Serializable]
    public class ValorInvalidoException : Exception
    {
        public ValorInvalidoException() : base("INVALID_VALUE: Valor de movimento deve ser maior que zero.")
        {
        }
    }
}