using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PizzaMais.Pizza.Core.Utils
{
    public static class SqlHelper
    {
        public static string ObterString(this Query query)
        {
            var compiler = new PostgresCompiler();
            SqlResult result = compiler.Compile(query);
            return RenomearParametros(result.Sql, result.Bindings);
        }

        private static string RenomearParametros(string sqlKata, List<object> bindings)
        {
            StringBuilder sb = new StringBuilder(sqlKata);

            for (int i = 0; i < bindings.Count; i++)
                sb.Replace("@p" + i.ToString(), bindings[i].ToString());

            return sb.ToString();
        }

        public static string Inserir(string tabela, IEnumerable<string> campos)
        {
            var script = "INSERT INTO public.\"" + tabela + "\"( \"Id\",";

            foreach (var campo in campos)
            {
                script += "\"" + campo + "\",";
            }

            script = script.TrimEnd(',') + ") VALUES(DEFAULT,";

            foreach (var valor in campos)
            {
                script += $"@{valor},";
            }

            script = script.TrimEnd(',') + ") RETURNING \"Id\";";

            return script;
        }

        public static string Delete(string tabela) => "DELETE FROM public.\"" + tabela + "\" WHERE \"Id\" = @Id";

        public static string DeleteBulk(string tabela) => $"DELETE FROM public.{tabela.FormatNpgsql()} WHERE ${"Id".FormatNpgsql()} in @Id";

        public static string Update(string tabela, IEnumerable<string> campos)
        {
            var script = "UPDATE public.\"" + tabela + "\" SET ";

            foreach (var campo in campos)
            {
                script += "\"" + campo + "\" = @" + campo + ",";
            }

            script = script.TrimEnd(',') + " WHERE \"Id\" = @Id";

            return script;
        }


        public static DataTable FormatarInsertBulk<T>(T[] values, out List<string> colunas)
        {
            var table = new DataTable();
            colunas = new List<string>();

            foreach (var prop in values[0].GetType().GetProperties())
            {
                if (prop.Name != "Id")
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    colunas.Add(prop.Name);
                }
            }

            for (var count = 0; count < values.Length; count++)
            {
                var dataRow = table.NewRow();
                var props = values[count].GetType().GetProperties().Where(x => x.Name != "Id").ToArray();
                for (var propCount = 0; propCount < props.Length; propCount++)
                {
                    var value = props[propCount].GetValue(values[count], null);
                    dataRow[propCount] = value ?? DBNull.Value;
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }


        public static string FormatNpgsql(this IEnumerable<string> campos) => String.Join(",", campos.Select(s => String.Format("\"{0}\"", s)));
        public static string FormatNpgsql(this string tabela) => String.Join(",", new string[] { tabela }.Select(s => String.Format("\"{0}\"", s)));
    }
}
