﻿using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class IngredienteSql
    {

        public static string Consulta(IngredienteFiltro filtro)
        {
            var query = new Query("Ingrediente")
                .Select("Id", "Nome", "Ativo");

            if (filtro.Id.HasValue)
                query.Where("Id", "@Id");

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

    }
}