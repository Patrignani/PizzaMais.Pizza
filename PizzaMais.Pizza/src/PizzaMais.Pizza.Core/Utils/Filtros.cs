using PizzaMais.Pizza.Communs.Enum;
using SqlKata;

namespace PizzaMais.Pizza.Core.Utils
{
    public static class Filtros
    {
        public static void TermoPesquisa(this Query query, TermoBusca? termoBusca, string variavel)
        {
            if (termoBusca.HasValue)
            {
                switch (termoBusca)
                {
                    case Communs.Enum.TermoBusca.eq:
                        query.Where("Preco", variavel);
                        break;
                    case Communs.Enum.TermoBusca.le:
                        query.WhereRaw($"Preco < {variavel}");
                        break;
                    case Communs.Enum.TermoBusca.gr:
                        query.WhereRaw($"Preco >{variavel}");
                        break;
                    case Communs.Enum.TermoBusca.greq:
                        query.WhereRaw($"Preco >= {variavel}");
                        break;
                    case Communs.Enum.TermoBusca.leeq:
                        query.WhereRaw($"Preco <= {variavel}");
                        break;
                }
            }
        }
    }
}
