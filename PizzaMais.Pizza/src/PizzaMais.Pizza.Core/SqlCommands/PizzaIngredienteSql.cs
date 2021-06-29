using PizzaMais.Pizza.Core.Utils;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public class PizzaIngredienteSql
    {
        public static string Delete() => SqlHelper.DeleteBulk("PizzaIngrediente");
    }
}
