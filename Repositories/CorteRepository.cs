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

        public async Task<int> AddCorteAsync(CorteDto corte)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "sp_AddCorte",
                new { corte.TotalPedidos, corte.TotalVentas, corte.Observaciones },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Corte>> GetAllCortesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Corte>(
                "sp_GetAllCortes",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Corte?> GetCorteByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Corte>(
                "sp_GetCorteById",
                new { IdCorte = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}