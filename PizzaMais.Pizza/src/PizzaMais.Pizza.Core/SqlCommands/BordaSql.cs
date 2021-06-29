using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;
using System.Linq;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class BordaSql
    {
        private static Query consultas() => new Query("Borda").Select("Id", "Preco", "Nome", "Ativo");

        public static string ObterPorId()
        {
            var query = consultas()
                   .Where("Id", "@Id");

            return query.ObterString();
        }

        public static string Consulta(BordaFiltro filtro)
        {
            var query = consultas();

            if (filtro.Id.HasValue)
                query.WhereRaw("CAST(\"Id\" AS VARCHAR(8)) LIKE CONCAT(CAST(@Id AS VARCHAR(8)),'%') ");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "LOWER(CONCAT(@Nome,'%'))");

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            if (filtro.Preco.HasValue)
                query.WhereRaw("\"Preco\" >= @Preco");

            query.MontarIngredienteOrderBy(filtro)
               .Offset(filtro.Offset)
               .Limit(filtro.Limit);

            return query.ObterString();
        }

        public static string Inserir() => SqlHelper.Inserir("Borda", new string[] {
            "Nome","Preco","Ativo", "DataCriacao", "UsuarioIdCriacao"
        });

        public static string Update() =>
            SqlHelper.Update("Borda", new string[] {
            "Nome", "Preco","Ativo", "DataAtualizacao", "UsuarioIdAtualizacao"
        });

        public static string Delete() => SqlHelper.Delete("Borda");

        public static Query MontarIngredienteOrderBy(this Query query, BordaFiltro filtro)
        {
            if (filtro.OrderbyAsc.Any())
            {
                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderBy("Nome");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderBy("Id");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "preco"))
                    query.OrderBy("Preco");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderBy("Ativo");
            }

            if (filtro.OrderbyDesc.Any())
            {
                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderByDesc("Nome");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderByDesc("Id");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "preco"))
                    query.OrderByDesc("Preco");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderByDesc("Ativo");
            }

            return query;
        }
    }
}
