using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Repository
{
    public interface IBordaRepository
    {
        Task<int> InserirAsync(Borda model);
        Task AtualizarAsync(Borda model);
        Task<IEnumerable<Borda>> LitarAsync(BordaFiltro filtro);
        Task<Borda> ObterAsync(int id);
        Task DeletarAsync(int id);
    }
}
