namespace PizzaMais.Pizza.Communs.Filters
{
    public class PizzaFiltro : FiltroBase
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public decimal? Preco { get; set; }
        public string Codigo { get; set; }
        public bool? Ativo { get; set; }
    }
}
