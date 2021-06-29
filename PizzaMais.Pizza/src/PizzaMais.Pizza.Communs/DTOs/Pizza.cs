using System;
using System.Collections.Generic;

namespace PizzaMais.Pizza.Communs.DTOs
{
    public class PizzaBasico
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public List<IngredienteLista> Ingredientes { get; set; } = new List<IngredienteLista>();
    }

    public class PizzaInserir : PizzaBasico
    {
        public Model.Pizza ObterModel(int usuarioId)
        {
            return new Model.Pizza
            {
                Ativo = Ativo,
                Codigo = Codigo,
                DataCriacao = DateTime.UtcNow,
                Nome = Nome,
                Preco = Preco,
                UsuarioIdCriacao = usuarioId,
                Ingredientes = Ingredientes
            };
        }
    }

    public class PizzaObter : PizzaBasico
    {
        public int Id { get; set; }

        public PizzaObter()
        { 
        
        }

        public PizzaObter(Model.Pizza pizza)
        {
            Codigo = pizza.Codigo;
            Nome = pizza.Nome;
            Preco = pizza.Preco;
            Id = pizza.Id;
            Ativo = pizza.Ativo;
            Ingredientes = pizza.Ingredientes;
        }
    }
}
