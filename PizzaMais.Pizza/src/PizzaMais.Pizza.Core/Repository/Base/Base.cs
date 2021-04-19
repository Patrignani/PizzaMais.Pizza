using System.Data;

namespace PizzaMais.Pizza.Core.Repository.Base
{
    internal abstract class Base
    {
        public Base(IDbConnection con, IDbTransaction tran)
        {
            _connection = con;
            _transaction = tran;
        }

        public readonly IDbConnection _connection;

        public readonly IDbTransaction _transaction;
    }
}
