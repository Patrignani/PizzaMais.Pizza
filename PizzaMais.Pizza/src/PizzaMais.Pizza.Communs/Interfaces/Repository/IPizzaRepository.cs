using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Repository
{
    public interface IPizzaRepository
    {
        Task<int> InserirAsync(Model.Pizza model);
        Task<PizzaObter> ObterPorIdAsync(int id);
        Task AtualizarAsync(Model.Pizza model);
        Task<IEnumerable<PizzaObter>> ListarAsync(PizzaFiltro filtro);
        Task DeletarAsync(int id);
    }
}
