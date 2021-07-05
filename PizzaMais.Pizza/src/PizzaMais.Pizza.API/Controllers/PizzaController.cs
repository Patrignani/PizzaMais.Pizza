using Microsoft.AspNetCore.Mvc;
using PizzaMais.Pizza.Communs.DTOs;
using PizzaMais.Pizza.Communs.Filters;
using PizzaMais.Pizza.Communs.Interfaces.Service;
using System.Threading.Tasks;

namespace PizzaMais.Pizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _service;

        public PizzaController(IPizzaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PizzaInserir value) => Ok(await _service.InserirAsync(value));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id) => Ok(await _service.ObterPorIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PizzaAtualizar value)
        {
            if (value.Id == id)
            {
                return Ok(await _service.AtualizarAsync(value));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletarAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PizzaFiltro filtro) => Ok(await _service.ListarAsync(filtro));
    }
}
