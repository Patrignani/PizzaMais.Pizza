using PizzaMais.Pizza.Communs.DTOs;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Repository
{
    public interface IPizzaRepository
    {
        Task<int> InserirAsync(Model.Pizza model);
        Task<PizzaObter> ObterPorIdAsync(int id);
    }
}
