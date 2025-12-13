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

        // GET: api/producto
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        // POST: api/producto
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductoDto productoDto)
        {
            var id = await _productoService.AddProductoAsync(productoDto);
            return CreatedAtAction(nameof(GetById), new { id }, productoDto);
        }

        // PUT: api/producto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDto productoDto)
        {
            var updated = await _productoService.UpdateProductoAsync(id, productoDto);

            if (!updated)
                return NotFound(); // si no se encontró el producto

            return Ok(new { message = "Producto actualizado correctamente" });
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productoService.DeleteProductoAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}