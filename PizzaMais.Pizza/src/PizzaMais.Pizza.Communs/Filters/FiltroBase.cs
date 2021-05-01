using System.Collections.Generic;

namespace PizzaMais.Pizza.Communs.Filters
{
    public class FiltroBase
    {
        public ushort Offset { get; set; }
        public ushort Limit { get; set; }
        public List<string> OrderbyAsc { get; set; } = new List<string>();
        public List<string> OrderbyDesc { get; set; } = new List<string>();
    }
}
