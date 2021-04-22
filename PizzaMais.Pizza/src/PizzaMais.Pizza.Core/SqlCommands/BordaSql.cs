using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class BordaSql
    {
        public static string Consulta(BordaFiltro filtro)
        {
            var query = new Query("Borda")
                .Select("Id", "Preco", "Nome", "Ativo");

            if (filtro.Id.HasValue)
                query.Where("Id", "@Id");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "@Nome + '%'");

            if (filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            if (filtro.Preco.HasValue)
            {
                if (filtro.TermoBuscaPreco.HasValue)
                {
                    query.TermoPesquisa(filtro.TermoBuscaPreco, "@Preco");
                }
                else
                {
                    query.Where("Preco", "@Preco");
                }
            }

            return query.ObterString();
        }

        public static string Inserir() =>
             @"INSERT INTO [dbo].[Borda]
            ([Nome]
            ,[Preco]
            ,[Ativo]
            ,[DataCriacao]
            ,[UsuarioIdCriacao])
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
    }
}
