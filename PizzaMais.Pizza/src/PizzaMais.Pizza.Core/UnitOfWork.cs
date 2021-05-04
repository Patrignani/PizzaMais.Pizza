using Npgsql;
using PizzaMais.Pizza.Communs.Interfaces;
using PizzaMais.Pizza.Communs.Interfaces.Repository;
using PizzaMais.Pizza.Core.Repository;
using System.Data;

namespace PizzaMais.Pizza.Core
{
    internal class UnitOfWork : IUnitOfWork
    {
        IDbConnection _connection = null;
        IDbTransaction _transaction = null;

        private IUnidadeMedidaRepository _unidadeMedidaRepository = null;
        private IIngredienteRepository _ingredienteRepository = null;
        private IBordaRepository _bordaRepository = null;

        public UnitOfWork(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public IUnidadeMedidaRepository UnidadeMedidaRepository => _unidadeMedidaRepository != null ? _unidadeMedidaRepository : _unidadeMedidaRepository = new UnidadeMedidaRepository(_connection, _transaction);
        public IIngredienteRepository IngredienteRepository => _unidadeMedidaRepository != null ? _ingredienteRepository : _ingredienteRepository = new IngredienteRepository(_connection, _transaction);
        public IBordaRepository BordaRepository => _unidadeMedidaRepository != null ? _bordaRepository : _bordaRepository = new BordaRepository(_connection, _transaction);

        public void Begin()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();

            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
