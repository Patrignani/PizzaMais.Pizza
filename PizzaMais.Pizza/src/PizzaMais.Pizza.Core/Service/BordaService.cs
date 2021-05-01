using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Communs.Interfaces.Service
{
    internal class BordaService : IBordaService
    {
        private readonly IUnitOfWork _uow;
        public BordaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task InserirAsync(Borda model)
        {
            model.DataCriacao = DateTime.UtcNow;
            model.UsuarioIdCriacao = 1;
            model.Id = await _uow.BordaRepository.InserirAsync(model);
        }

        public async Task AtualizarAsync(Borda model)
        {
            model.DataAtualizacao = DateTime.UtcNow;
            model.UsuarioIdAtualizacao = 1;
            await _uow.BordaRepository.AtualizarAsync(model);
        }

        public async Task DeletarAsync(int id) => await _uow.BordaRepository.DeletarAsync(id);

        public async Task<Borda> ObterPorIdAsync(int id) => await _uow.BordaRepository.ObterAsync(id);

        public async Task<IEnumerable<Borda>> ListarAsync(BordaFiltro filtro) =>
            await _uow.BordaRepository.LitarAsync(filtro);
    }
}
