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
        private readonly IImageService _imageService;

        public ProductoController(IProductoService productoService, IImageService imageService)
        {
            _productoService = productoService;
            _imageService = imageService;
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

        // 🔹 Endpoint para subir imagen
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No se recibió ninguna imagen" });

            try
            {
                var url = await _imageService.SaveImageAsync(file);
                return Ok(new { url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductoDto productoDto)
        {
            // Si la imagen empieza con "/uploads", conviértela en URL completa
            if (!string.IsNullOrEmpty(productoDto.Imagen) && productoDto.Imagen.StartsWith("/uploads"))
            {
                var request = HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                productoDto.Imagen = $"{baseUrl}{productoDto.Imagen}";
            }

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