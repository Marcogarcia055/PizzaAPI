using Pizzeria.Dto;
using Pizzeria.Models;
using Pizzeria.Repositories.Interface;
using Pizzeria.Service.Interface;

namespace Pizzeria.Service
{
    public class CorteService : ICorteService
    {
        private readonly ICorteRepository _corteRepository;

        public CorteService(ICorteRepository corteRepository)
        {
            _corteRepository = corteRepository;
        }

        public Task<int> AddCorteAsync(CorteDto corte)
            => _corteRepository.AddCorteAsync(corte);

        public Task<IEnumerable<Corte>> GetAllCortesAsync()
            => _corteRepository.GetAllCortesAsync();

        public Task<Corte?> GetCorteByIdAsync(int id)
            => _corteRepository.GetCorteByIdAsync(id);
    }
}