using Pizzeria.Models;
using Pizzeria.Dto;

namespace Pizzeria.Repositories.Interface
{
    public interface ICorteRepository
    {
        Task<CorteResultDto> AddCorteAsync(CorteDto corte);
        Task<IEnumerable<CorteGetAllDto>> GetAllCortesAsync();
        Task<Corte?> GetCorteByIdAsync(int id);
    }
}