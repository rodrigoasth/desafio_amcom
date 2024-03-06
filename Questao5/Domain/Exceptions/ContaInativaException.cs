namespace Questao5.Domain.Exceptions
{
    public class ContaInativaException : Exception
    {
        public ContaInativaException() : base("INACTIVE_ACCOUNT: Conta inativa")
        {
        }
    }
}
