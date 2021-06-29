using PizzaMais.Pizza.Communs.DTOs;
using System.Collections.Generic;

namespace PizzaMais.Pizza.Communs.Model
{
    public class Pizza : Base
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public List<IngredienteLista> Ingredientes { get; set; } = new List<IngredienteLista>();
    }
}
