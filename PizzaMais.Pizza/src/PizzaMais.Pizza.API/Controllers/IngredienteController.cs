using Microsoft.AspNetCore.Mvc;
using PizzaMais.Pizza.Communs.filters;
using PizzaMais.Pizza.Communs.Interfaces.Service;
using PizzaMais.Pizza.Communs.Model;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteService _service;

        public IngredienteController(IIngredienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]IngredienteFiltro filtro) => Ok(await _service.ListarAsync(filtro));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id) => Ok(await _service.ObterPorIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Ingrediente value)
        {
            await _service.InserirAsync(value);

            return Ok(value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Ingrediente value)
        {
            if (value.Id == id)
            {
                await _service.AtualizarAsync(value);

                return Ok(value);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletarAsync(id);

            return Ok();
        }
    }
}
