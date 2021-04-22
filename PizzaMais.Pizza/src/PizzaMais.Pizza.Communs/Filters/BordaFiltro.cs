using PizzaMais.Pizza.Communs.Enum;

namespace PizzaMais.Pizza.Communs.filters
{
    public class BordaFiltro
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Ativo { get; set; }
        public decimal? Preco{ get; set; }
        public TermoBusca? TermoBuscaPreco { get; set;}
    }
}
