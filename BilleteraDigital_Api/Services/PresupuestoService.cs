using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilleteraDigital_Api.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly string _connectionString;

        public PresupuestoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexion")!;
        }

        public async Task<IEnumerable<PresupuestoResponse>> ObtenerTodosAsync()
        {
            var lista = new List<PresupuestoResponse>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_Presupuesto_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new PresupuestoResponse
                            {
                                Id = reader.GetInt32("Id"),
                                Monto = reader.GetDecimal("Monto"),
                                Categoria = reader.GetString("Categoria"),
                                InicioPresupuesto = reader.GetDateTime("InicioPresupuesto"),
                                FinalPresupuesto = reader.GetDateTime("FinalPresupuesto"),
                                UsuarioId = reader.GetInt32("UsuarioId"),
                                Estado = reader.GetInt32("Estado")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<PresupuestoResponse?> ObtenerPorIdAsync(int id)
        {
            PresupuestoResponse? dto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_Presupuesto_ObtenerPorld", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dto = new PresupuestoResponse
                            {
                                Id = reader.GetInt32("Id"),
                                Monto = reader.GetDecimal("Monto"),
                                Categoria = reader.GetString("Categoria"),
                                InicioPresupuesto = reader.GetDateTime("InicioPresupuesto"),
                                FinalPresupuesto = reader.GetDateTime("FinalPresupuesto"),
                                UsuarioId = reader.GetInt32("UsuarioId"),
                                Estado = reader.GetInt32("Estado")
                            };
                        }
                    }
                }
            }
            return dto;
        }

        public async Task<bool> CrearAsync(PresupuestoResponse presupuesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_Presupuesto_Registrar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Monto", presupuesto.Monto);
                    command.Parameters.AddWithValue("@Categoria", presupuesto.Categoria);
                    command.Parameters.AddWithValue("@InicioPresupuesto", presupuesto.InicioPresupuesto);
                    command.Parameters.AddWithValue("@FinalPresupuesto", presupuesto.FinalPresupuesto);
                    command.Parameters.AddWithValue("@UsuarioId", presupuesto.UsuarioId);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> ActualizarAsync(PresupuestoResponse presupuesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_Presupuesto_Editar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", presupuesto.Id);
                    command.Parameters.AddWithValue("@Monto", presupuesto.Monto);
                    command.Parameters.AddWithValue("@Categoria", presupuesto.Categoria);
                    command.Parameters.AddWithValue("@InicioPresupuesto", presupuesto.InicioPresupuesto);
                    command.Parameters.AddWithValue("@FinalPresupuesto", presupuesto.FinalPresupuesto);

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
                using (var command = new SqlCommand("sp_Presupuesto_Eliminar", connection))
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