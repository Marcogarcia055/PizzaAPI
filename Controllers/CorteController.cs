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
            var id = await _corteService.AddCorteAsync(corteDto);
            return CreatedAtAction(nameof(GetById), new { id }, corteDto);
        }
    }
}