using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Pizzeria.Models;
using Pizzeria.Dto;
using Pizzeria.Repositories.Interface;

namespace Pizzeria.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString = string.Empty;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task<int> AddProductoAsync(ProductoDto producto)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "sp_AddProducto",
                new { producto.Nombre, producto.Precio, producto.Imagen },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Producto>(
                "sp_GetAllProducto",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Producto?> GetProductoByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Producto>(
                "sp_GetByIdProducto",
                new { IdProducto = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateProductoAsync(int id, ProductoDto producto)
        {
            using var connection = new SqlConnection(_connectionString);
            var rows = await connection.ExecuteAsync(
                "sp_UpdateProducto",
                new { IdProducto = id, producto.Nombre, producto.Precio, producto.Imagen },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rows = await connection.ExecuteAsync(
                "sp_DeleteProducto",
                new { IdProducto = id },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }
    }
}