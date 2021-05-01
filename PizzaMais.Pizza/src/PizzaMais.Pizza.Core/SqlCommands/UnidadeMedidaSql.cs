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
                query.WhereRaw("CAST(Id AS NVARCHAR) LIKE CAST(@Id AS NVARCHAR) + '%' ");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "@Nome + '%'");

            if (!String.IsNullOrEmpty(filtro.Sigla))
                query.WhereLike("Sigla", "@Sigla + '%'");

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            query.MontarUnidadeMedidaOrderBy(filtro)
               .Offset(filtro.Offset)
               .Limit(filtro.Limit);

            return query.ObterString();
        }

        public static string Inserir() =>
            @"INSERT INTO [dbo].[UnidadeMedida]
            ([Nome]
            ,[Sigla]
            ,[Ativo]
            ,[DataCriacao]
            ,[UsuarioIdCriacao])
        OUTPUT Inserted.Id
         VALUES
            (@Nome,
            @Sigla,
            @Ativo,
            @DataCriacao,
            @UsuarioIdCriacao)";

        public static string Update() =>
            @"UPDATE [dbo].[UnidadeMedida]
            SET [Nome] = @Nome
            ,[Sigla] = @Sigla
            ,[Ativo] = @Ativo
            ,[DataAtualizacao] = @DataAtualizacao
            ,[UsuarioIdAtualizacao] = @UsuarioIdAtualizacao
            WHERE 
            [Id] = @Id";

        public static string Delete() =>
            @"DELETE [dbo].[UnidadeMedida]  WHERE [Id] = @Id";

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
