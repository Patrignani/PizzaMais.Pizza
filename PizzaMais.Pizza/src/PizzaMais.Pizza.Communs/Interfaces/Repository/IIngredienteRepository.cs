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
        Task<IEnumerable<Ingrediente>> LitarAsync(IngredienteFiltro ingredienteFiltro);
        Task<Ingrediente> ObterAsync(int id);
        Task DeletarAsync(int id);

    }
}
