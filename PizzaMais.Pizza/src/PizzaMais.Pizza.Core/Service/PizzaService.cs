using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.Filters;
using PizzaMais.Pizza.Communs.Interfaces;
using PizzaMais.Pizza.Communs.Interfaces.Service;
using PizzaMais.Pizza.Communs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.Core.Service
{
    internal class PizzaService : IPizzaService
    {
        private readonly IUnitOfWork _uow;
        public PizzaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PizzaObter> AtualizarAsync(PizzaAtualizar model)
        {
            _uow.Begin();
            try
            {
                var entidade = model.ObterModel(1);
                await _uow.PizzaRepository.AtualizarAsync(entidade).ConfigureAwait(false);
                await CriarIngredientesAsync(entidade);
                await AdicionarIngredientesAsync(entidade);
                await DeletarIngredientesAsync(entidade);

                _uow.Commit();

                return new PizzaObter(entidade);
            }
            catch (Exception e)
            {
                throw e.GetBaseException();
            }
        }

        public async Task<PizzaObter> InserirAsync(PizzaInserir pizza)
        {
            _uow.Begin();
            try
            {
                var model = pizza.ObterModel(1);
                model.Id = await _uow.PizzaRepository.InserirAsync(model).ConfigureAwait(false);
                await CriarIngredientesAsync(model);
                await AdicionarIngredientesAsync(model);

                _uow.Commit();

                return new PizzaObter(model);
            }
            catch (Exception e)
            {
                throw e.GetBaseException();
            }
        }

        public async Task<IEnumerable<PizzaObter>> ListarAsync(PizzaFiltro filtro) => await _uow.PizzaRepository.ListarAsync(filtro).ConfigureAwait(false);

        public async Task DeletarAsync(int id) => await _uow.PizzaRepository.DeletarAsync(id);

        public async Task<PizzaObter> ObterPorIdAsync(int id) => await _uow.PizzaRepository.ObterPorIdAsync(id).ConfigureAwait(false);

        private async Task AdicionarIngredientesAsync(Communs.Model.Pizza model)
        {
            var pizzaIngredientes = model.Ingredientes.Where(x => x.Status == Communs.Enum.StatusLista.Novo && x.Id > 0).Select(x => new PizzaIngrediente
            {
                PizzaId = model.Id,
                IngredienteId = x.Id
            });

            if (pizzaIngredientes.Any())
            {
                await _uow.PizzaIngredienteRepository.InserirLoteAsync(pizzaIngredientes).ConfigureAwait(false);
            }

            var itens = model.Ingredientes.Where(x => x.Status == Communs.Enum.StatusLista.Novo && x.Id > 0).ToList();
            itens.Select(x =>
            {
                x.Status = Communs.Enum.StatusLista.Default;
                return x;
            });
            itens.AddRange(model.Ingredientes.Where(x => x.Status != Communs.Enum.StatusLista.Novo).ToList());

            model.Ingredientes = itens;
        }

        private async Task CriarIngredientesAsync(Communs.Model.Pizza model)
        {
            var pizzaIngredientes = model.Ingredientes.Where(x => x.Status == Communs.Enum.StatusLista.Novo && x.Id == 0).Select(x => new Ingrediente
            {
                Ativo = true,
                DataCriacao = DateTime.UtcNow,
                Nome = x.Text,
                UsuarioIdCriacao = 1
            });

            if (pizzaIngredientes.Any())
            {
                await _uow.IngredienteRepository.InserirLoteAsync(pizzaIngredientes).ConfigureAwait(false);
                var ingredientes = await _uow.IngredienteRepository.ListarPorNomeAsync(pizzaIngredientes.Select(x => x.Nome)).ConfigureAwait(false);
                foreach (var ingrediente in pizzaIngredientes)
                {
                    ingrediente.Id = ingredientes.Where(x => x.Nome == ingrediente.Nome).Select(x => x.Id).FirstOrDefault();
                }

                var itens = model.Ingredientes.Where(x => x.Id != 0).ToList();
                itens.AddRange(ingredientes.Select(x => new IngredienteLista
                {
                    Id = x.Id,
                    Text = x.Nome,
                    Status = Communs.Enum.StatusLista.Novo
                }));

                model.Ingredientes = itens;
            }
        }

        private async Task DeletarIngredientesAsync(Communs.Model.Pizza model)
        {
            var deletar = model.Ingredientes.Where(x => x.Status == Communs.Enum.StatusLista.Excluir).Select(x => x.Id);
            if (deletar.Any())
                await _uow.PizzaIngredienteRepository.DeletarLoteAsync(model.Id, deletar);

            model.Ingredientes = model.Ingredientes.Where(x => x.Status != Communs.Enum.StatusLista.Excluir).ToList();
        }
    }
}
