using Pizzeria.Models;
using Pizzeria.Dto;

namespace Pizzeria.Repositories.Interface
{
    public interface IPedidoRepository
    {
        Task<int> AddPedidoConDetallesAsync(PedidoDto pedido);
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido?> GetPedidoByIdAsync(int id);
        Task<bool> DeletePedidoAsync(int id);
    }
}