using System;

namespace PizzaMais.Pizza.Communs.Model
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioIdCriacao { get; set; }
        public int? UsuarioIdAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}
