using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Service
{
    public interface IBordaService
    {
        Task InserirAsync(Borda model);
        Task AtualizarAsync(Borda model);
        Task DeletarAsync(int id);
        Task<Borda> ObterPorIdAsync(int id);
        Task<IEnumerable<Borda>> ListarAsync(BordaFiltro filtro);
    }
}
