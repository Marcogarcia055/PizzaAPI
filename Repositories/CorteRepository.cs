using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Pizzeria.Models;
using Pizzeria.Dto;
using Pizzeria.Repositories.Interface;

namespace Pizzeria.Repositories
{
    public class CorteRepository : ICorteRepository
    {
        private readonly string _connectionString = string.Empty;

        public CorteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task<CorteResultDto> AddCorteAsync(CorteDto corte)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<CorteResultDto>(
                "sp_AddCorte",
                new { corte.Observaciones },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<CorteGetAllDto>> GetAllCortesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CorteGetAllDto>(
                "sp_GetAllCortes",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Corte?> GetCorteByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using (var multi = await connection.QueryMultipleAsync(
                "sp_GetCorteById",
                new { IdCorte = id },
                commandType: CommandType.StoredProcedure))
            {
                var corte = await multi.ReadFirstOrDefaultAsync<Corte>();
                var pedidos = (await multi.ReadAsync<Pedido>()).ToList();

                if (corte != null)
                    corte.Pedidos = pedidos;

                return corte;
            }
        }
    }
}