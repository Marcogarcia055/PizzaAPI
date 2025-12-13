using Pizzeria.Models;
using Pizzeria.Dto;

namespace Pizzeria.Repositories.Interface
{
    public interface ICorteRepository
    {
        Task<int> AddCorteAsync(CorteDto corte);
        Task<IEnumerable<Corte>> GetAllCortesAsync();
        Task<Corte?> GetCorteByIdAsync(int id);
    }
}