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
            var compiler = new SqlServerCompiler();
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
    }
}
