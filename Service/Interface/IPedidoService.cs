using Pizzeria.Dto;
using Pizzeria.Models;

namespace Pizzeria.Service.Interface
{
    public interface IPedidoService
    {
        Task<int> AddPedidoConDetallesAsync(PedidoDto pedido);
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<PedidoDto?> GetPedidoByIdAsync(int id);
        Task<int> DeletePedidoAsync(int id);
    }
}