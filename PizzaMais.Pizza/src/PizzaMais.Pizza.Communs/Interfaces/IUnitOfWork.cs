using PizzaMais.Pizza.Communs.Interfaces.Repository;
using System;

namespace PizzaMais.Pizza.Communs.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUnidadeMedidaRepository UnidadeMedidaRepository { get; }
        IBordaRepository BordaRepository { get; }
        IIngredienteRepository IngredienteRepository { get; }

        void Begin();
        void Rollback();
        void Commit();
    }
}
