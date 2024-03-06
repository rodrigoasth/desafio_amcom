using System.ComponentModel;

namespace Questao5.Domain.Enumerators
{
    public enum TipoMovimento
    {
        [Description("C")]
        Credito=1,
        [Description("D")]
        Debito=2
    }
}
