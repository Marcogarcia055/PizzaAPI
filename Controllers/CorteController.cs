using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto;
using Pizzeria.Service.Interface;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorteController : ControllerBase
    {
        private readonly ICorteService _corteService;

        public CorteController(ICorteService corteService)
        {
            _corteService = corteService;
        }

        // GET: api/corte
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cortes = await _corteService.GetAllCortesAsync();
            return Ok(cortes);
        }

        // GET: api/corte/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var corte = await _corteService.GetCorteByIdAsync(id);
            if (corte == null) return NotFound();
            return Ok(corte);
        }

        // POST: api/corte
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CorteDto corteDto)
        {
            var result = await _corteService.AddCorteAsync(corteDto);

            // Si no hay pedidos pendientes, devolvemos BadRequest con el mensaje
            if (result.NuevoId == null)
                return BadRequest(new { message = result.Mensaje });

            // Si se creó el corte, devolvemos 201 con el objeto completo
            return CreatedAtAction(nameof(GetById), new { id = result.NuevoId }, result);
        }
    }
}