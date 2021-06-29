using PizzaMais.Pizza.Communs.DTOs;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Service
{
    public interface IPizzaService
    {
        Task<PizzaObter> InserirAsync(PizzaInserir pizza);
        Task<PizzaObter> ObterPorIdAsync(int id);
    }
}
