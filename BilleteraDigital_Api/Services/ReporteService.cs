using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilleteraDigital_Api.Services
{
    public class ReporteService : IReporteService
    {
        private readonly string _connectionString;

        public ReporteService(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "No se encontró la cadena de conexión 'DefaultConnection'.");
        }

        public async Task<List<ReporteMovimientoResponse>> ObtenerMovimientosAsync(
            int idUsuario,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null,
            int? idTipoMovimiento = null,
            int? idCategoria = null)
        {
            var lista = new List<ReporteMovimientoResponse>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(
                    "sp_Reporte_Movimientos",
                    connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IdUsuario", SqlDbType.Int)
                        .Value = idUsuario;

                    command.Parameters.Add("@FechaInicio", SqlDbType.Date)
                        .Value = fechaInicio?.Date ?? (object)DBNull.Value;

                    command.Parameters.Add("@FechaFin", SqlDbType.Date)
                        .Value = fechaFin?.Date ?? (object)DBNull.Value;

                    command.Parameters.Add("@IdTipoMovimiento", SqlDbType.Int)
                        .Value = idTipoMovimiento ?? (object)DBNull.Value;

                    command.Parameters.Add("@IdCategoria", SqlDbType.Int)
                        .Value = idCategoria ?? (object)DBNull.Value;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var movimiento = new ReporteMovimientoResponse
                            {
                                IdMovimiento = reader.GetInt32(
                                    reader.GetOrdinal("IdMovimiento")),

                                FechaMovimiento = reader.GetDateTime(
                                    reader.GetOrdinal("FechaMovimiento")),

                                TipoMovimiento = reader.GetString(
                                    reader.GetOrdinal("TipoMovimiento")),

                                Categoria = reader.GetString(
                                    reader.GetOrdinal("Categoria")),

                                Descripcion = reader.IsDBNull(
                                    reader.GetOrdinal("Descripcion"))
                                    ? null
                                    : reader.GetString(
                                        reader.GetOrdinal("Descripcion")),

                                Monto = reader.GetDecimal(
                                    reader.GetOrdinal("Monto")),

                                Usuario = reader.GetString(
                                    reader.GetOrdinal("Usuario"))
                            };

                            lista.Add(movimiento);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<List<ReporteCategoriaResponse>> ObtenerCategoriasAsync(
    int idUsuario)
        {
            var lista = new List<ReporteCategoriaResponse>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(
                    "sp_Reporte_Categorias",
                    connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IdUsuario", SqlDbType.Int)
                        .Value = idUsuario;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new ReporteCategoriaResponse
                            {
                                IdCategoria = reader.GetInt32(
                                    reader.GetOrdinal("IdCategoria")),

                                Nombre = reader.GetString(
                                    reader.GetOrdinal("Nombre"))
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
