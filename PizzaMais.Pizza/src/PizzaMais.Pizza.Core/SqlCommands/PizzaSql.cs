using PizzaMais.Pizza.Core.Utils;
using SqlKata;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class PizzaSql
    {
        private static Query consultas() => new Query("Pizza").Select("Pizza.Id", "Pizza.Codigo", "Pizza.Nome", "Pizza.Preco", "Pizza.Ativo");


        public static string Inserir() => SqlHelper.Inserir("Pizza", new string[] {
            "Nome", "Codigo","Preco","Ativo", "DataCriacao", "UsuarioIdCriacao"
        });

        public static string ObterPorId()
        {
            var query = consultas()
                .Select("Ingrediente.Nome AS Text","Ingrediente.Id")
                .Join("PizzaIngrediente", j => j.On("PizzaIngrediente.PizzaId", "Pizza.Id"))
                .Join("Ingrediente", j => j.On("PizzaIngrediente.IngredienteId", "Ingrediente.Id"))
                .Where("Pizza.Id", "@Id");

            return query.ObterString();
        }
    }
}
