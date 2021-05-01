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
                query.WhereRaw("CAST(Id AS NVARCHAR) LIKE CAST(@Id AS NVARCHAR) + '%' ");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "@Nome + '%'");

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            if (filtro.Preco.HasValue)
                query.WhereRaw("Preco >= @Preco");

            query.MontarIngredienteOrderBy(filtro)
               .Offset(filtro.Offset)
               .Limit(filtro.Limit);

            return query.ObterString();
        }

        public static string Inserir() =>
             @"INSERT INTO [dbo].[Borda]
            ([Nome]
            ,[Preco]
            ,[Ativo]
            ,[DataCriacao]
            ,[UsuarioIdCriacao])
         OUTPUT Inserted.Id
         VALUES
            (@Nome,
            @Preco,
            @Ativo,
            @DataCriacao,
            @UsuarioIdCriacao)";

        public static string Update() =>
            @"UPDATE [dbo].[Borda]
            SET [Nome] = @Nome
            ,[Preco] = @Preco
            ,[Ativo] = @Ativo
            ,[DataAtualizacao] = @DataAtualizacao
            ,[UsuarioIdAtualizacao] = @UsuarioIdAtualizacao
            WHERE 
            [Id] = @Id";

        public static string Delete() =>
            @"DELETE [dbo].[Borda]  WHERE [Id] = @Id";

        public static Query MontarIngredienteOrderBy(this Query query, BordaFiltro filtro)
        {
            if (filtro.OrderbyAsc.Any())
            {
                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderBy("Nome");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderBy("Id");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "preco"))
                    query.OrderBy("preco");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderBy("Ativo");
            }

            if (filtro.OrderbyDesc.Any())
            {
                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "nome"))
                    query.OrderByDesc("Nome");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "id"))
                    query.OrderByDesc("Id");

                if (filtro.OrderbyAsc.Any(x => x.ToLower().Trim() == "preco"))
                    query.OrderByDesc("preco");

                if (filtro.OrderbyDesc.Any(x => x.ToLower().Trim() == "ativo"))
                    query.OrderByDesc("Ativo");

            }

            return query;
        }
    }
}
