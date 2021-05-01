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
    internal class UnidadeMedidaRepository : Base.Base, IUnidadeMedidaRepository
    {
        public UnidadeMedidaRepository(IDbConnection con, IDbTransaction tran) : base(con, tran)
        {

        }

        public async Task<int> InserirAsync(UnidadeMedida model) =>
            await _connection.ExecuteScalarAsync<int>(UnidadeMedidaSql.Inserir(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task AtualizarAsync(UnidadeMedida model) =>
            await _connection.ExecuteAsync(UnidadeMedidaSql.Update(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task<IEnumerable<UnidadeMedida>> LitarAsync(UnidadeMedidaFiltro unidadeMedidaFiltro) =>
            await _connection.QueryAsync<UnidadeMedida>(UnidadeMedidaSql.Consulta(unidadeMedidaFiltro), unidadeMedidaFiltro, transaction: _transaction).ConfigureAwait(false);

        public async Task<UnidadeMedida> ObterAsync(int id) =>
            await _connection.QueryFirstOrDefaultAsync<UnidadeMedida>(UnidadeMedidaSql.ObterPorId(), new { Id = id }, transaction: _transaction).ConfigureAwait(false);

        public async Task DeletarAsync(int id) => await _connection.ExecuteAsync(UnidadeMedidaSql.Delete(),new {Id =id }).ConfigureAwait(false);
    }
}
