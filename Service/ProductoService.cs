using Pizzeria.Dto;
using Pizzeria.Models;
using Pizzeria.Repositories.Interface;
using Pizzeria.Service.Interface;

namespace Pizzeria.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public Task<int> AddProductoAsync(ProductoDto producto)
            => _productoRepository.AddProductoAsync(producto);

        public Task<IEnumerable<Producto>> GetAllProductosAsync()
            => _productoRepository.GetAllProductosAsync();

        public Task<Producto?> GetProductoByIdAsync(int id)
            => _productoRepository.GetProductoByIdAsync(id);

        public Task<int> UpdateProductoAsync(int id, ProductoDto producto)
            => _productoRepository.UpdateProductoAsync(id, producto);

        public Task<int> DeleteProductoAsync(int id)
            => _productoRepository.DeleteProductoAsync(id);
    }
}