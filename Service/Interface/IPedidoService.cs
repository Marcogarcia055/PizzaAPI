using Pizzeria.Dto;
using Pizzeria.Models;

namespace Pizzeria.Service.Interface
{
    public interface IPedidoService
    {
        Task<int> AddPedidoConDetallesAsync(PedidoDto pedido);
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido?> GetPedidoByIdAsync(int id);
        Task<bool> DeletePedidoAsync(int id);
    }
}