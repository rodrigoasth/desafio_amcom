namespace Questao5.Domain.Exceptions
{
    public class ContaInvalidaException : Exception
    {
        public ContaInvalidaException() : base("INVALID_ACCOUNT: Conta inválida")
        {
        }        
    }
}
