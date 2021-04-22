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
    internal class IngredienteRepository : Base.Base, IIngredienteRepository
    {
        public IngredienteRepository(IDbConnection con, IDbTransaction tran) : base(con, tran)
        {

        }

        public async Task<int> InserirAsync(Ingrediente model) =>
         await _connection.ExecuteScalarAsync<int>(IngredienteSql.Inserir(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task AtualizarAsync(Ingrediente model) =>
            await _connection.ExecuteAsync(IngredienteSql.Update(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task<IEnumerable<Ingrediente>> LitarAsync(IngredienteFiltro ingredienteFiltro) =>
            await _connection.QueryAsync<Ingrediente>(IngredienteSql.Consulta(ingredienteFiltro), ingredienteFiltro, transaction: _transaction).ConfigureAwait(false);

        public async Task<Ingrediente> ObterAsync(IngredienteFiltro ingredienteFiltro) =>
            await _connection.QueryFirstOrDefaultAsync<Ingrediente>(IngredienteSql.Consulta(ingredienteFiltro), ingredienteFiltro, transaction: _transaction).ConfigureAwait(false);

        public async Task DeletarAsync(int id) => await _connection.ExecuteAsync(IngredienteSql.Delete(), new { Id = id }).ConfigureAwait(false);
    }
}
