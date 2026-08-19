using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using BilleteraDigital_Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilleteraDigital_Api.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly string _connectionString;

        public PresupuestoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
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
                                IdPresupuesto = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                Usuario = reader.GetString(2),
                                IdCategoria = reader.GetInt32(3),
                                Categoria = reader.GetString(4),
                                MontoLimite = reader.GetDecimal(5),
                                MontoGastado = reader.GetDecimal(6),
                                MontoDisponible = reader.GetDecimal(7),
                                FechaInicio = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaInicio"))),
                                FechaFin = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaFin"))),
                                Estado = reader.GetBoolean(10),
                                FechaRegistro = reader.GetDateTime(11)                            
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
                using (var command = new SqlCommand("sp_Presupuesto_ObtenerPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdPresupuesto", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dto = new PresupuestoResponse
                            {
                                IdPresupuesto = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                Usuario = reader.GetString(2),
                                IdCategoria = reader.GetInt32(3),
                                Categoria = reader.GetString(4),
                                MontoLimite = reader.GetDecimal(5),
                                MontoGastado = reader.GetDecimal(6),
                                MontoDisponible = reader.GetDecimal(7),
                                FechaInicio = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaInicio"))),
                                FechaFin = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaFin"))),
                                Estado = reader.GetBoolean(10),
                                FechaRegistro = reader.GetDateTime(11)
                            };
                        }
                    }
                }
            }
            return dto;
        }

        public async Task<bool> CrearAsync(PresupuestoRequest presupuesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_Presupuesto_Registrar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdUsuario", presupuesto.IdUsuario);
                    command.Parameters.AddWithValue("@IdCategoria", presupuesto.IdCategoria);
                    command.Parameters.AddWithValue("@MontoLimite", presupuesto.MontoLimite);
                    command.Parameters.AddWithValue("@MontoGastado", presupuesto.MontoGastado);
                    command.Parameters.AddWithValue("@FechaInicio", presupuesto.FechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", presupuesto.FechaFin);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> ActualizarAsync(PresupuestoRequestUpdate presupuesto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_Presupuesto_Editar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(
                        "@IdPresupuesto",
                        presupuesto.IdPresupuesto
                    );

                    command.Parameters.AddWithValue(
                        "@IdCategoria",
                        presupuesto.IdCategoria
                    );

                    command.Parameters.AddWithValue(
                        "@MontoLimite",
                        presupuesto.MontoLimite
                    );

                    command.Parameters.AddWithValue(
                        "@MontoGastado",
                        presupuesto.MontoGastado
                    );

                    command.Parameters.AddWithValue(
                        "@FechaInicio",
                        presupuesto.FechaInicio
                    );

                    command.Parameters.AddWithValue(
                        "@FechaFin",
                        presupuesto.FechaFin
                    );

                    int filasAfectadas =
                        await command.ExecuteNonQueryAsync();

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
                    command.Parameters.AddWithValue("@IdPresupuesto", id);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}