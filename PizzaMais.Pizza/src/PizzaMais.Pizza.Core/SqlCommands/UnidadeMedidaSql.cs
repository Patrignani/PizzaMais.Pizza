using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;
using System.Linq;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class UnidadeMedidaSql
    {
        private static Query consultas() => new Query("UnidadeMedida").Select("Id", "Nome", "Sigla", "Ativo");

        public static string ObterPorId()
        {
            var query = consultas()
                   .Where("Id", "@Id");

            return query.ObterString();
        }

        public static string Consulta(UnidadeMedidaFiltro filtro)
        {
            var query = consultas();

            if (filtro.Id.HasValue)
                query.WhereRaw("CAST(\"Id\" AS VARCHAR(8)) LIKE CONCAT(CAST(@Id AS VARCHAR(8)),'%') ");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "LOWER(CONCAT(@Nome,'%'))");

            if (!String.IsNullOrEmpty(filtro.Sigla))
                query.WhereLike("Sigla", "LOWER(CONCAT(@Sigla,'%'))");

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            query.MontarUnidadeMedidaOrderBy(filtro)
               .Offset(filtro.Offset)
               .Limit(filtro.Limit);

            return query.ObterString();
        }

        public static string Inserir() => SqlHelper.Inserir("UnidadeMedida", new string[] {
            "Nome", "Sigla","Ativo", "DataCriacao", "UsuarioIdCriacao"
        });

        public static string Update() => SqlHelper.Update("UnidadeMedida", new string[] {
            "Nome", "Sigla", "Ativo", "DataAtualizacao", "UsuarioIdAtualizacao"
        });

        public static string Delete() => SqlHelper.Delete("UnidadeMedida");


        public static Query MontarUnidadeMedidaOrderBy(this Query query, UnidadeMedidaFiltro filtro)
        {
            if (filtro.OrderbyAsc.Any())
            {
                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderBy("Nome");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "sigla"))
                    query.OrderBy("Sigla");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderBy("Id");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderBy("Ativo");
            }

            if (filtro.OrderbyDesc.Any())
            {
                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderByDesc("Nome");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "sigla"))
                    query.OrderByDesc("Sigla");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderByDesc("Id");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderByDesc("Ativo");

            }
            return query;
        }

    }
}
