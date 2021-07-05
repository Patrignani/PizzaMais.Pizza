using System.Data;
using System.Threading.Tasks;
using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using PizzaMais.Pizza.Communs.Interfaces.Repository;
using Dapper;
using PizzaMais.Pizza.Core.SqlCommands;
using System.Linq;

namespace PizzaMais.Pizza.Core.Repository
{
    internal class PizzaIngredienteRepository : Base.Base, IPizzaIngredienteRepository
    {
        public PizzaIngredienteRepository(IDbConnection con, IDbTransaction tran) : base(con, tran)
        {
        }

        public async Task InserirLoteAsync(IEnumerable<PizzaIngrediente> models) => await InsertBulkAsync(models, "PizzaIngrediente");

        public async Task DeletarLoteAsync(int pizzaId, IEnumerable<int> IngredienteIds) => await _connection.ExecuteAsync(PizzaIngredienteSql.Delete(),
            new { PizzaId = pizzaId, IngredienteIds = IngredienteIds.ToList() },
            transaction: _transaction);
    }
}
