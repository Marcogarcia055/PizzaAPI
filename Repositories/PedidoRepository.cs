using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Pizzeria.Models;
using Pizzeria.Dto;
using Pizzeria.Repositories.Interface;

namespace Pizzeria.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task<int> AddPedidoConDetallesAsync(PedidoDto pedido)
        {
            using var connection = new SqlConnection(_connectionString);

            // Convertir lista de detalles a DataTable para pasarlo como TVP
            var detallesTable = new DataTable();
            detallesTable.Columns.Add("IdProducto", typeof(int));
            detallesTable.Columns.Add("Cantidad", typeof(int));
            detallesTable.Columns.Add("PrecioUnitario", typeof(decimal));

            foreach (var d in pedido.Detalles)
            {
                detallesTable.Rows.Add(d.IdProducto, d.Cantidad, d.PrecioUnitario);
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Cliente", pedido.Cliente);
            parameters.Add("@Fecha", pedido.Fecha);
            parameters.Add("@Detalles", detallesTable.AsTableValuedParameter("DetallePedidoType"));

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddPedidoConDetalles",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Pedido>(
                "sp_GetAllPedido",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<PedidoDto?> GetPedidoByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            using var multi = await connection.QueryMultipleAsync(
                "sp_GetByIdPedido",
                new { IdPedido = id },
                commandType: CommandType.StoredProcedure
            );

            // Primer result set: pedido
            var pedido = await multi.ReadFirstOrDefaultAsync<PedidoDto>();
            if (pedido == null) return null;

            // Segundo result set: detalles
            var detalles = (await multi.ReadAsync<DetallePedidoDto>()).ToList();
            pedido.Detalles = detalles;

            return pedido;
        }

        public async Task<int> DeletePedidoAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<int>(
                "sp_DeletePedido",
                new { IdPedido = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}