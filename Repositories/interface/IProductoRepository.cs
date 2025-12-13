using Pizzeria.Models;
using Pizzeria.Dto;

namespace Pizzeria.Repositories.Interface
{
    public interface IProductoRepository
    {
        Task<int> AddProductoAsync(ProductoDto producto);
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto?> GetProductoByIdAsync(int id);
        Task<bool> UpdateProductoAsync(int id, ProductoDto producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}