﻿using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Core.Utils;
using SqlKata;
using System;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public static class UnidadeMedidaSql
    {

        public static string Consulta(UnidadeMedidaFiltro filtro)
        {
            var query = new Query("UnidadeMedida")
                .Select("Id", "Nome", "Ativo");

            if (filtro.Id.HasValue)
                query.Where("Id", "@Id");

            if (!String.IsNullOrEmpty(filtro.Nome))
                query.WhereLike("Nome", "@Nome + '%'");

            if(filtro.Ativo.HasValue)
                query.Where("Ativo", "@Ativo");

            return query.ObterString();

        }

        public static string Inserir() =>
            @"INSERT INTO [dbo].[UnidadeMedida]
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
            @"UPDATE [dbo].[UnidadeMedida]
            SET [Nome] = @Nome
            ,[Ativo] = @Ativo
            ,[DataAtualizacao] = @DataAtualizacao
            ,[UsuarioIdAtualizacao] = @UsuarioIdAtualizacao
            WHERE 
            [Id] = @Id";

        public static string Delete() =>
            @"DELETE [dbo].[UnidadeMedida]  WHERE [Id] = @Id";

    }
}