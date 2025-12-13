using Pizzeria.Dto;
using Pizzeria.Models;

namespace Pizzeria.Service.Interface
{
    public interface ICorteService
    {
        Task<int> AddCorteAsync(CorteDto corte);
        Task<IEnumerable<Corte>> GetAllCortesAsync();
        Task<Corte?> GetCorteByIdAsync(int id);
    }
}