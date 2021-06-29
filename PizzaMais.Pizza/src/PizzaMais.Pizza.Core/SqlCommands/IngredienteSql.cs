using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;
using System.Linq;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class IngredienteSql
    {

        private static Query consultas() => new Query("Ingrediente").Select("Id", "Nome", "Ativo");

        public static string ObterPorId()
        {
            var query = consultas()
                   .Where("Id", "@Id");

            return query.ObterString();
        }

        public static string ConsultaSimpificada(IngredienteFiltro filtro)
        {
            var query = new Query("Ingrediente").Select("Id", "Nome");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "LOWER(CONCAT(@Nome,'%'))");

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            query
                .Limit(8)
                .OrderBy("Nome");

            return query.ObterString();
        }

        public static string ObterPorNomes()
        {
            var query = consultas()
                 .WhereRaw("\"Nome\" = any(@Nomes)");

            return query.ObterString();
        }

        public static string Consulta(IngredienteFiltro filtro)
        {
            var query = consultas();

            if (filtro.Id.HasValue)
                query.WhereRaw("CAST(\"Id\" AS VARCHAR(8)) LIKE CONCAT(CAST(@Id AS VARCHAR(8)),'%') ");

            if (!String.IsNullOrEmpty(filtro.Nome))
            {
                query.WhereLike("Nome", "LOWER(CONCAT(@Nome,'%'))");
            }
            else if (!String.IsNullOrEmpty(filtro.NomeIgual))
            {
                query.Where("Nome", "@NomeIgual");
            }

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            query.MontarIngredienteOrderBy(filtro)
                .Offset(filtro.Offset)
                .Limit(filtro.Limit);

            return query.ObterString();
        }

        public static string Inserir() => SqlHelper.Inserir("Ingrediente", new string[] {
            "Nome","Ativo", "DataCriacao", "UsuarioIdCriacao"
        });

        public static string Update() => SqlHelper.Update("Ingrediente", new string[] {
            "Nome", "Ativo", "DataAtualizacao", "UsuarioIdAtualizacao"
        });

        public static string Delete() => SqlHelper.Delete("Ingrediente");

        public static Query MontarIngredienteOrderBy(this Query query, IngredienteFiltro filtro)
        {
            if (filtro.OrderbyAsc.Any())
            {
                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderBy("Nome");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderBy("Id");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderBy("Ativo");
            }

            if (filtro.OrderbyDesc.Any())
            {
                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderByDesc("Nome");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderByDesc("Id");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderByDesc("Ativo");

            }
            return query;
        }

    }
}
