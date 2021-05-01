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

        public static string Consulta(IngredienteFiltro filtro)
        {
            var query = consultas();

            if (filtro.Id.HasValue)
                query.WhereRaw("CAST(Id AS NVARCHAR) LIKE CAST(@Id AS NVARCHAR) + '%' ");

            if (!String.IsNullOrEmpty(filtro.Nome))
            {
                query.WhereLike("Nome", "@Nome + '%'");
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

        public static string Inserir() =>
            @"INSERT INTO [dbo].[Ingrediente]
            ([Nome]
            ,[Ativo]
            ,[DataCriacao]
            ,[UsuarioIdCriacao])
        OUTPUT Inserted.Id
         VALUES
            (@Nome,
            @Ativo,
            @DataCriacao,
            @UsuarioIdCriacao)";

        public static string Update() =>
            @"UPDATE [dbo].[Ingrediente]
            SET [Nome] = @Nome
            ,[Ativo] = @Ativo
            ,[DataAtualizacao] = @DataAtualizacao
            ,[UsuarioIdAtualizacao] = @UsuarioIdAtualizacao
            WHERE 
            [Id] = @Id";

        public static string Delete() =>
            @"DELETE [dbo].[Ingrediente]  WHERE [Id] = @Id";

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
