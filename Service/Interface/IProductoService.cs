using Pizzeria.Dto;
using Pizzeria.Models;

namespace Pizzeria.Service.Interface
{
    public interface IProductoService
    {
        Task<int> AddProductoAsync(ProductoDto producto);
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto?> GetProductoByIdAsync(int id);
        Task<bool> UpdateProductoAsync(int id, ProductoDto producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}