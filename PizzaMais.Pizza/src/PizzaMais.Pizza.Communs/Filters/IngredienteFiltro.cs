using PizzaMais.Pizza.Communs.Filters;

namespace PizzaMais.Pizza.Communs.filters
{
    public class IngredienteFiltro : FiltroBase
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Ativo { get; set; }
        public string NomeIgual { get; set; }
    }
}
