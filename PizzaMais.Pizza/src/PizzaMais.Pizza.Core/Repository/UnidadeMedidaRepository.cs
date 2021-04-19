using PizzaMais.Pizza.Communs.Interfaces.Repository;
using System.Data;

namespace PizzaMais.Pizza.Core.Repository
{
    internal class UnidadeMedidaRepository : Base.Base, IUnidadeMedidaRepository
    {
        public UnidadeMedidaRepository(IDbConnection con, IDbTransaction tran) : base(con, tran)
        {

        }
    }
}
