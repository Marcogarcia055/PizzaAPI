using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto;
using Pizzeria.Service.Interface;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/pedido
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoService.GetAllPedidosAsync();
            return Ok(pedidos);
        }

        // GET: api/pedido/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        // POST: api/pedido
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PedidoDto pedidoDto)
        {
            var id = await _pedidoService.AddPedidoConDetallesAsync(pedidoDto);
            return CreatedAtAction(nameof(GetById), new { id }, pedidoDto);
        }

        // DELETE: api/pedido/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rowsAffected = await _pedidoService.DeletePedidoAsync(id);

            if (rowsAffected == 0)
                return NotFound(new { message = "Pedido no encontrado" });

            return Ok(new { message = "Pedido eliminado correctamente" });
        }
    }
}