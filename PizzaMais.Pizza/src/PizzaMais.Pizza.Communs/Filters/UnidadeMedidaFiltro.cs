using PizzaMais.Pizza.Communs.Filters;

namespace PizzaMais.Pizza.Communs.filters
{
    public class UnidadeMedidaFiltro : FiltroBase
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool? Ativo { get; set; }
    }
}
