using PizzaMais.Pizza.Communs.Enum;
using SqlKata;

namespace PizzaMais.Pizza.Core.Utils
{
    public static class Filtros
    {
        public static void TermoPesquisa(this Query query, TermoBusca? termoBusca, string variavel, string campo)
        {
            if (termoBusca.HasValue)
            {
                switch (termoBusca)
                {
                    case Communs.Enum.TermoBusca.eq:
                        query.Where(campo, variavel);
                        break;
                    case Communs.Enum.TermoBusca.le:
                        query.WhereRaw($"{campo} < {variavel}");
                        break;
                    case Communs.Enum.TermoBusca.gr:
                        query.WhereRaw($"{campo} > {variavel}");
                        break;
                    case Communs.Enum.TermoBusca.greq:
                        query.WhereRaw($"{campo} >= {variavel}");
                        break;
                    case Communs.Enum.TermoBusca.leeq:
                        query.WhereRaw($"{campo} <= {variavel}");
                        break;
                }
            }
        }
    }
}
