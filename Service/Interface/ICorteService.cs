using Pizzeria.Dto;
using Pizzeria.Models;

namespace Pizzeria.Service.Interface
{
    public interface ICorteService
    {
        Task<CorteResultDto> AddCorteAsync(CorteDto corte);
        Task<IEnumerable<CorteGetAllDto>> GetAllCortesAsync();
        Task<Corte?> GetCorteByIdAsync(int id);
    }
}