using Microsoft.AspNetCore.Mvc;
using PizzaMais.Pizza.Communs.DTOs;
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
    }
}
