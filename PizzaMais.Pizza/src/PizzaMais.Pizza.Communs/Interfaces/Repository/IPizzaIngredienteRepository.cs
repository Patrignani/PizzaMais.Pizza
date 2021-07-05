using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Repository
{
    public interface IPizzaIngredienteRepository
    {
        Task InserirLoteAsync(IEnumerable<PizzaIngrediente> models);
        Task DeletarLoteAsync(int pizzaId, IEnumerable<int> IngredienteIds);
    }
}
