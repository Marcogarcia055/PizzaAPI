using Pizzeria.Models;
using Pizzeria.Dto;

namespace Pizzeria.Repositories.Interface
{
    public interface IPedidoRepository
    {
        Task<int> AddPedidoConDetallesAsync(PedidoDto pedido);
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<PedidoDto?> GetPedidoByIdAsync(int id);
        Task<int> DeletePedidoAsync(int id);
    }
}