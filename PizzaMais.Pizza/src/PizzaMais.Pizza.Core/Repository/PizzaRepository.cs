using Dapper;
using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.Interfaces.Repository;
using PizzaMais.Pizza.Core.SqlCommands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Core.Repository
{
    internal class PizzaRepository : Base.Base, IPizzaRepository
    {
        public PizzaRepository(IDbConnection con, IDbTransaction tran) : base(con, tran)
        {

        }

        public async Task<int> InserirAsync(Communs.Model.Pizza model) =>
            await _connection.ExecuteScalarAsync<int>(PizzaSql.Inserir(), model, transaction: _transaction).ConfigureAwait(false);

        public async Task<PizzaObter> ObterPorIdAsync(int id)
        {
            var pizzaDictionary = new Dictionary<int, PizzaObter>();

            try
            {
                var pizza = (await _connection.QueryAsync<PizzaObter, IngredienteLista, PizzaObter>(PizzaSql.ObterPorId(),
                     (pizza, ingrediente) =>
                     {
                         PizzaObter PizzaObterEntidade;

                         if (!pizzaDictionary.TryGetValue(pizza.Id, out PizzaObterEntidade))
                         {
                             PizzaObterEntidade = pizza;
                             pizzaDictionary.Add(PizzaObterEntidade.Id, PizzaObterEntidade);
                         }

                         PizzaObterEntidade.Ingredientes.Add(ingrediente);
                         return PizzaObterEntidade;
                     }, new { Id = id },
                     splitOn: "Text", transaction: _transaction).ConfigureAwait(false)).Distinct().ToList();

                return pizza.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
