using SqlKata;
using SqlKata.Compilers;
using System.Collections.Generic;
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

        public static string Delete(string tabela) => "DELETE FROM public.\""+ tabela + "\" WHERE \"Id\" = @Id";

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
    }
}
