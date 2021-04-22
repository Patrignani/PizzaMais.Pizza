using Dapper;
using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Interfaces.Repository;
using PizzaMais.Pizza.Communs.Model;
using PizzaMais.Pizza.Core.SqlCommands;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Core.Repository
{
    internal class BordaRepository : Base.Base, IBordaRepository
    {
        public BordaRepository(IDbConnection con, IDbTransaction tran) : base(con, tran)
        {

        }

        public async Task<int> InserirAsync(Borda model) =>
            await _connection.ExecuteScalarAsync<int>(BordaSql.Inserir(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task AtualizarAsync(Borda model) =>
            await _connection.ExecuteAsync(BordaSql.Update(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task<IEnumerable<Borda>> LitarAsync(BordaFiltro filtro) =>
            await _connection.QueryAsync<Borda>(BordaSql.Consulta(filtro), filtro, transaction: _transaction).ConfigureAwait(false);

        public async Task<Borda> ObterAsync(BordaFiltro filtro) =>
            await _connection.QueryFirstOrDefaultAsync<Borda>(BordaSql.Consulta(filtro), filtro, transaction: _transaction).ConfigureAwait(false);

        public async Task DeletarAsync(int id) => 
            await _connection.ExecuteAsync(BordaSql.Delete(), new { Id = id }).ConfigureAwait(false);
    }
}
