using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Service
{
    public interface IUnidadeMedidaService
    {
        Task InserirAsync(UnidadeMedida model);
        Task AtualizarAsync(UnidadeMedida model);
        Task DeletarAsync(int id);
        Task<UnidadeMedida> ObterPorIdAsync(int id);
        Task<IEnumerable<UnidadeMedida>> ListarAsync(UnidadeMedidaFiltro filtro);
    }
}
