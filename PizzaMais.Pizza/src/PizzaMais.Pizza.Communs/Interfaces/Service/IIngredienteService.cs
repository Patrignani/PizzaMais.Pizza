using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Service
{
    public interface IIngredienteService
    {
        Task InserirAsync(Ingrediente model);
        Task AtualizarAsync(Ingrediente model);
        Task DeletarAsync(int id);
        Task<Ingrediente> ObterPorIdAsync(int id);
        Task<IEnumerable<Ingrediente>> ListarAsync(IngredienteFiltro filtro);
        Task<Ingrediente> ObterOuInserirAsync(string nome);
        Task<IEnumerable<IngredienteSimplificado>> LitarSimplificadoAsync(IngredienteFiltro filtro);
    }
}
