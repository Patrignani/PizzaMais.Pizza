using PizzaMais.Pizza.Communs.Enum;

namespace PizzaMais.Pizza.Communs.DTOs
{
    public class IngredienteLista
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public StatusLista Status { get; set;}
    }

    public class IngredienteSimplificado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
