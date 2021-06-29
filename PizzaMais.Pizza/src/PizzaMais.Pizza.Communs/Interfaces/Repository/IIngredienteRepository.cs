using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Repository
{
    public interface IIngredienteRepository
    {
        Task<int> InserirAsync(Ingrediente model);
        Task AtualizarAsync(Ingrediente model);
        Task<IEnumerable<Ingrediente>> ListarAsync(IngredienteFiltro ingredienteFiltro);
        Task<Ingrediente> ObterAsync(int id);
        Task DeletarAsync(int id);
        Task InserirLoteAsync(IEnumerable<Ingrediente> models);
        Task<IEnumerable<Ingrediente>> ListarPorNomeAsync(IEnumerable<string> nome);
        Task<IEnumerable<IngredienteSimplificado>> LitarSimplificadoAsync(IngredienteFiltro filtro);
    }
}
