using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilleteraDigital_Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly string _connectionString;

        public DashboardService(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "No se encontró la cadena de conexión 'conexion'.");
        }

        public async Task<DashboardResumenResponse> ObtenerResumenAsync(int idUsuario)
        {
            var resultado = new DashboardResumenResponse();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(
                "sp_Dashboard_Resumen",
                connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                resultado.TotalIngresos =
                    Convert.ToDecimal(reader["TotalIngresos"]);

                resultado.TotalGastos =
                    Convert.ToDecimal(reader["TotalGastos"]);

                resultado.Saldo =
                    Convert.ToDecimal(reader["Saldo"]);
            }

            return resultado;
        }

        public async Task<IEnumerable<DashboardCategoriaResponse>>
            ObtenerResumenPorCategoriaAsync(int idUsuario)
        {
            var lista = new List<DashboardCategoriaResponse>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(
                "sp_Movimiento_ResumenPorCategoria",
                connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new DashboardCategoriaResponse
                {
                    Categoria = reader["Categoria"]?.ToString() ?? "",
                    Total = Convert.ToDecimal(reader["Total"])
                });
            }

            return lista;
        }

        public async Task<IEnumerable<DashboardTipoResponse>>
            ObtenerResumenPorTipoAsync(int idUsuario)
        {
            var lista = new List<DashboardTipoResponse>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(
                "sp_Movimiento_ResumenPorTipo",
                connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new DashboardTipoResponse
                {
                    Tipo = reader["TipoMovimiento"]?.ToString() ?? "",
                    Total = Convert.ToDecimal(reader["Total"])
                });
            }

            return lista;
        }
    }
}
