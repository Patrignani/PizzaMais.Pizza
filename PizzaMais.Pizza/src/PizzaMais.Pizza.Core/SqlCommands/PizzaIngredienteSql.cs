using PizzaMais.Pizza.Core.Utils;

namespace PizzaMais.Pizza.Core.SqlCommands
{
    public class PizzaIngredienteSql
    {
        public static string Delete() => $"DELETE FROM public.{"PizzaIngrediente".FormatNpgsql()} WHERE {"PizzaId".FormatNpgsql()} = @PizzaId AND {"IngredienteId".FormatNpgsql()} = ANY(@IngredienteIds)";
    }
}
