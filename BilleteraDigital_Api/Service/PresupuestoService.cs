using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilleteraDigital_Api.Service
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly string _connectionString;

        public PresupuestoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexion")!;
        }

        public async Task<IEnumerable<PresupuestoDTO>> ObtenerTodosAsync()
        {
            var lista = new List<PresupuestoDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ListarPresupuestos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new PresupuestoDTO
                            {
                                Id = reader.GetInt32("Id"),
                                Monto = reader.GetDecimal("Monto"),
                                Categoria = reader.GetString("Categoria"),
                                Fecha = reader.GetDateTime("Fecha"),
                                Estado = reader.GetInt32("Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<PresupuestoDTO?> ObtenerPorIdAsync(int id)
        {
            PresupuestoDTO? dto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ObtenerPresupuestoPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dto = new PresupuestoDTO
                            {
                                Id = reader.GetInt32("Id"),
                                Monto = reader.GetDecimal("Monto"),
                                Categoria = reader.GetString("Categoria"),
                                Fecha = reader.GetDateTime("Fecha"),
                                Estado = reader.GetInt32("Estado")
                            };
                        }
                    }
                }
            }
            return dto;
        }

        public async Task<bool> CrearAsync(PresupuestoDTO presupuesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_InsertarPresupuesto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Monto", presupuesto.Monto);
                    command.Parameters.AddWithValue("@Categoria", presupuesto.Categoria);
                    command.Parameters.AddWithValue("@Fecha", presupuesto.Fecha);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> ActualizarAsync(PresupuestoDTO presupuesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ActualizarPresupuesto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", presupuesto.Id);
                    command.Parameters.AddWithValue("@Monto", presupuesto.Monto);
                    command.Parameters.AddWithValue("@Categoria", presupuesto.Categoria);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> EliminarLogicoAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_EliminarPresupuestoLogico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}