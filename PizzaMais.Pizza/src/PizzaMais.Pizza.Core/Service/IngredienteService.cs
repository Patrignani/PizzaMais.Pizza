using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Interfaces;
using PizzaMais.Pizza.Communs.Interfaces.Service;
using PizzaMais.Pizza.Communs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Core.Service
{
    internal class IngredienteService : IIngredienteService
    {
        private readonly IUnitOfWork _uow;
        public IngredienteService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task InserirAsync(Ingrediente model)
        {
            model.DataCriacao = DateTime.UtcNow;
            model.UsuarioIdCriacao = 1;
            model.Id = await _uow.IngredienteRepository.InserirAsync(model);
        }

        public async Task AtualizarAsync(Ingrediente model)
        {
            model.DataAtualizacao = DateTime.UtcNow;
            model.UsuarioIdAtualizacao = 1;
            await _uow.IngredienteRepository.AtualizarAsync(model);
        }

        public async Task DeletarAsync(int id) => await _uow.IngredienteRepository.DeletarAsync(id);
        public async Task<Ingrediente> ObterPorIdAsync(int id) => await _uow.IngredienteRepository.ObterAsync(id);

        public async Task<IEnumerable<Ingrediente>> ListarAsync(IngredienteFiltro filtro) =>
            await _uow.IngredienteRepository.LitarAsync(filtro);

        public async Task<Ingrediente> ObterOuInserirAsync(string nome)
        {
            var filtro = new IngredienteFiltro
            {
                NomeIgual = nome.Trim()
            };
            var model = (await _uow.IngredienteRepository.LitarAsync(filtro)).FirstOrDefault();

            if (model == null)
            {
                model = new Ingrediente
                {
                    Ativo = true,
                    Nome = nome,
                };

                await InserirAsync(model);
            }

            return model;
        }

    }
}
