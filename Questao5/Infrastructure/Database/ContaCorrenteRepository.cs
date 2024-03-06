using Dapper;
using Microsoft.Data.Sqlite;
using NSubstitute.Core;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Collections.Specialized;

namespace Questao5.Infrastructure.Database
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this._databaseConfig = databaseConfig;
        }

        public async Task<ContaCorrente> ConsultarSaldoAsync(int numeroConta)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var query = @"SELECT 
                            C.IDCONTACORRENTE AS Id,
                            M.idmovimento AS IdMov,
                            C.NUMERO AS NumeroConta,
                            C.NOME AS NomeTitular,
                            C.ATIVO AS Ativo
                        FROM CONTACORRENTE C LEFT JOIN MOVIMENTO M ON C.IDCONTACORRENTE = M.IDCONTACORRENTE
                        WHERE C.NUMERO = @numeroConta;";
            /*
            var contaDictionary = new Dictionary<Guid, ContaCorrente>();

            var contaCorrente = await connection.QueryAsync<ContaCorrente, Movimento, ContaCorrente>(query, (conta, movimento) => {

                if (contaDictionary.TryGetValue(conta.Id, out var contaExistente))
                {
                    conta = contaExistente;
                }
                else
                {
                    contaDictionary.Add(conta.Id, conta);
                }

                conta.Movimentos.Add(movimento);
                return conta;
            }, new { numeroConta }, splitOn: "IdMov");
            */
            var contaCorrente = await connection.QueryAsync<ContaCorrente>(query, new { numeroConta });
            return contaCorrente.FirstOrDefault();
        }

        public Guid AdicionarMovimentoAsync(Movimento movimento)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ContaExisteAsync(Guid idConta)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var query = "SELECT 1 FROM CONTACORRENTE WHERE IDCONTACORRENTE = @idConta";
            var contaCorrente = await connection.QueryAsync<int>(query, new { idConta });

            return contaCorrente != null;
        }

        public Task<bool> ContaExisteAsync(int numeroConta)
        {
            throw new NotImplementedException();
        }

        public async Task<ContaCorrente> ObterConta(Guid idConta)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var query = "SELECT * FROM CONTACORRENTE WHERE IDCONTACORRETE = @idConta";
            var contaCorrente = await connection.QueryAsync<ContaCorrente>(query, new { idConta });

            return contaCorrente.FirstOrDefault();
        }

        public Task<ContaCorrente> ObterConta(int numeroConta)
        {
            throw new NotImplementedException();
        }
    }
}
