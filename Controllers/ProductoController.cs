using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto;
using Pizzeria.Service.Interface;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductoDto productoDto)
        {
            var id = await _productoService.AddProductoAsync(productoDto);
            return CreatedAtAction(nameof(GetById), new { id }, productoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDto productoDto)
        {
            var rowsAffected = await _productoService.UpdateProductoAsync(id, productoDto);

            if (rowsAffected == 0)
                return NotFound(new { message = "Producto no encontrado" });

            return Ok(new { message = "Producto actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rowsAffected = await _productoService.DeleteProductoAsync(id);

            if (rowsAffected == 0)
                return NotFound(new { message = "Producto no encontrado" });

            return Ok(new { message = "Producto eliminado correctamente" });
        }
    }
}