using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Repository
{
    public interface IUnidadeMedidaRepository
    {
        Task<int> InserirAsync(UnidadeMedida model);
        Task AtualizarAsync(UnidadeMedida model);
        Task<IEnumerable<UnidadeMedida>> LitarAsync(UnidadeMedidaFiltro unidadeMedidaFiltro);
        Task<UnidadeMedida> ObterAsync(UnidadeMedidaFiltro unidadeMedidaFiltro);
        Task DeletarAsync(int id);
    }
}
