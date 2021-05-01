using PizzaMais.Pizza.Communs.Enum;
using PizzaMais.Pizza.Communs.Filters;

namespace PizzaMais.Pizza.Communs.filters
{
    public class BordaFiltro : FiltroBase
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Ativo { get; set; }
        public decimal? Preco{ get; set; }
    }
}
