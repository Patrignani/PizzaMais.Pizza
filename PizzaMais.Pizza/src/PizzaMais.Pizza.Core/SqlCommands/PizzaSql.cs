using PizzaMais.Pizza.Communs.Filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;
using System.Linq;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class PizzaSql
    {
        private static Query consultas() => new Query("Pizza").Select("Pizza.Id", "Pizza.Codigo", "Pizza.Nome", "Pizza.Preco", "Pizza.Ativo");


        public static string Inserir() => SqlHelper.Inserir("Pizza", new string[] {
            "Nome", "Codigo","Preco","Ativo", "DataCriacao", "UsuarioIdCriacao"
        });

        public static string Update() => SqlHelper.Update("Pizza", new string[] {
          "Nome", "Codigo","Preco","Ativo", "DataAtualizacao", "UsuarioIdAtualizacao"});

        public static string ObterPorId()
        {
            var query = consultas()
                .Select("Ingrediente.Nome AS Text", "Ingrediente.Id")
                .Join("PizzaIngrediente", j => j.On("PizzaIngrediente.PizzaId", "Pizza.Id"))
                .Join("Ingrediente", j => j.On("PizzaIngrediente.IngredienteId", "Ingrediente.Id"))
                .Where("Pizza.Id", "@Id");

            return query.ObterString();
        }

        public static string Delete() => SqlHelper.Delete("Pizza");

        public static string Consulta(PizzaFiltro filtro)
        {
            var query = consultas();

            if (filtro.Id.HasValue)
                query.WhereRaw("CAST(\"Pizza\".\"Id\" AS VARCHAR(8)) LIKE CONCAT(CAST(@Id AS VARCHAR(8)),'%') ");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Pizza.Nome", "LOWER(CONCAT(@Nome,'%'))");

            if (!String.IsNullOrEmpty(filtro.Codigo))
                query.WhereLike("Pizza.Codigo", "LOWER(CONCAT(@Codigo,'%'))");

            if (filtro.Preco.HasValue)
                query.WhereRaw("\"Pizza\".\"Preco\" >= @Preco");

            if (filtro.Ativo.HasValue)
                query.Where("Pizza.Ativo", "@Ativo");

            query.MontarPizzaOrderBy(filtro)
                .Offset(filtro.Offset)
                .Limit(filtro.Limit);

            return query.ObterString();
        }

        public static Query MontarPizzaOrderBy(this Query query, PizzaFiltro filtro)
        {
            if (filtro.OrderbyAsc.Any())
            {
                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderBy("Pizza.Nome");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderBy("Pizza.Id");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderBy("Pizza.Ativo");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "codigo"))
                    query.OrderBy("Pizza.Codigo");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "preco"))
                    query.OrderBy("Pizza.Preco");

            }

            if (filtro.OrderbyDesc.Any())
            {
                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderByDesc("Pizza.Nome");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderByDesc("Pizza.Id");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderByDesc("Pizza.Ativo");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "codigo"))
                    query.OrderByDesc("Pizza.Codigo");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "preco"))
                    query.OrderByDesc("Pizza.Preco");

            }

            return query;
        }
    }
}
