using Dapper;
using System;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public class SqliteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }

        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }
    }
}
