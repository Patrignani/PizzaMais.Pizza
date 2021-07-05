using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Service
{
    public interface IPizzaService
    {
        Task<PizzaObter> InserirAsync(PizzaInserir pizza);
        Task<PizzaObter> ObterPorIdAsync(int id);
        Task<PizzaObter> AtualizarAsync(PizzaAtualizar model);
        Task<IEnumerable<PizzaObter>> ListarAsync(PizzaFiltro filtro);
        Task DeletarAsync(int id);
    }
}
