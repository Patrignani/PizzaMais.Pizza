using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Interfaces;
using PizzaMais.Pizza.Communs.Interfaces.Service;
using PizzaMais.Pizza.Communs.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Core.Service
{
    internal class UnidadeMedidaService : IUnidadeMedidaService
    {
        private readonly IUnitOfWork _uow;
        public UnidadeMedidaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task InserirAsync(UnidadeMedida model)
        {
            model.DataCriacao = DateTime.UtcNow;
            model.UsuarioIdCriacao = 1;
            model.Id = await _uow.UnidadeMedidaRepository.InserirAsync(model);
        }

        public async Task AtualizarAsync(UnidadeMedida model)
        {
            model.DataAtualizacao = DateTime.UtcNow;
            model.UsuarioIdAtualizacao = 1;
            await _uow.UnidadeMedidaRepository.AtualizarAsync(model);
        }

        public async Task DeletarAsync(int id) => await _uow.UnidadeMedidaRepository.DeletarAsync(id);

        public async Task<UnidadeMedida> ObterPorIdAsync(int id) => await _uow.UnidadeMedidaRepository.ObterAsync(id);

        public async Task<IEnumerable<UnidadeMedida>> ListarAsync(UnidadeMedidaFiltro filtro) =>
            await _uow.UnidadeMedidaRepository.LitarAsync(filtro);
    }
}
