using Npgsql;
using System.Collections.Generic;
using System.Data;
using PizzaMais.Pizza.Core.Utils;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;

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


        public async Task InsertBulkAsync<T>(IEnumerable<T> objeto, string tabela)
        {
            var dataTable = SqlHelper.FormatarInsertBulk(objeto.ToArray(), out var colunas);
            var npgsqlConn = (NpgsqlConnection)_connection;
            tabela = tabela.FormatNpgsql();
            var columns = colunas.FormatNpgsql();

            var commandFormat = string.Format(CultureInfo.InvariantCulture, "COPY {0} {1} FROM STDIN BINARY", tabela, $"({columns})");
            using (var writer = npgsqlConn.BeginBinaryImport(commandFormat))
            {
                foreach (DataRow item in dataTable.Rows)
                {
                    await writer.WriteRowAsync(values:item.ItemArray).ConfigureAwait(false);
                }

                writer.Complete();
            }
        }
    }
}
