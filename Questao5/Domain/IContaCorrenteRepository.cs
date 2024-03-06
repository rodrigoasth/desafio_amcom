using Questao5.Domain.Entities;

namespace Questao5.Domain
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> ConsultarSaldoAsync(int numeroConta);
        Task<bool> ContaExisteAsync(Guid idConta);
        Task<bool> ContaExisteAsync(int numeroConta);
        Guid AdicionarMovimentoAsync(Movimento movimento);
        Task<ContaCorrente> ObterConta(Guid idConta);
        Task<ContaCorrente> ObterConta(int numeroConta);
    }
}
