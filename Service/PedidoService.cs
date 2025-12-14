using Pizzeria.Dto;
using Pizzeria.Models;
using Pizzeria.Repositories.Interface;
using Pizzeria.Service.Interface;

namespace Pizzeria.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public Task<int> AddPedidoConDetallesAsync(PedidoDto pedido)
            => _pedidoRepository.AddPedidoConDetallesAsync(pedido);

        public Task<IEnumerable<Pedido>> GetAllPedidosAsync()
            => _pedidoRepository.GetAllPedidosAsync();

        public Task<PedidoDto?> GetPedidoByIdAsync(int id)
            => _pedidoRepository.GetPedidoByIdAsync(id);

        public Task<int> DeletePedidoAsync(int id)
            => _pedidoRepository.DeletePedidoAsync(id);
    }
}