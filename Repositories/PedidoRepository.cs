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
        private readonly string _connectionString = string.Empty;

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

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Pedido>(
                "sp_GetByIdPedido",
                new { IdPedido = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rows = await connection.ExecuteAsync(
                "sp_DeletePedido",
                new { IdPedido = id },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }
    }
}