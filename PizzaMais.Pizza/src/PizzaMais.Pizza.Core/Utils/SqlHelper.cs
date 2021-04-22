using SqlKata;
using SqlKata.Compilers;

namespace PizzaMais.Pizza.Core.Utils
{
    public static class SqlHelper
    {
        public static string ObterString(this Query query)
        {
            var compiler = new SqlServerCompiler();
            SqlResult result = compiler.Compile(query);
            return result.Sql;
        }
    }
}
