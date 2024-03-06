using System;

namespace Questao1
{
    class ContaBancaria {
        public ContaBancaria(int numeroConta, string nomeTitular)
        {
            ValidarContaBancaria(numeroConta, nomeTitular);
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = 0;
        }
        public ContaBancaria(int numeroConta, string nomeTitular, double depositoInicial)
        {
            ValidarContaBancaria(numeroConta, nomeTitular);
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = depositoInicial;
        }

        private double _tarifa = 3.50;

        public int NumeroConta { get; init; }
        public string NomeTitular { get; private set; }
        public double Saldo { get; private set; }

        public void Deposito(double valor)
        {
            ValidarOperacaoBancaria(valor);

            Saldo += valor;
        }

        public void Saque(double valor)
        {
            ValidarOperacaoBancaria(valor);

            Saldo -= valor; 
            Saldo -= _tarifa;
        }

        private void ValidarContaBancaria(int numeroConta, string nomeTitular)
        {
            if (numeroConta <= 0) throw new Exception("O número de cadastro da conta bancária deve ser maior que zero!");

            if (nomeTitular.Length < 3 || 
                nomeTitular == string.Empty) throw new Exception("A quantidade de caracteres para nome do titular deve ser 3 ou maior!");
        }

        private void ValidarOperacaoBancaria(double valor)
        {
            if (valor <= 0) throw new Exception("Valor de operação bancária deve ser maior que 0!");
        }

        public override string ToString()
        {
            return String.Format("Conta: {0}, Titular: {1}, Saldo: {2}", NumeroConta, NomeTitular, Saldo);
        }
    }
}
