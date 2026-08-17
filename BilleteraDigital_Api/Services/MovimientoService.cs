using BilleteraDigital_Api.Interfaces;
using BilleteraDigital_Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BilleteraDigital_Api.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly string _connectionString;

        public MovimientoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Insertar(Movimiento m)
        {
            if (m.Monto <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.");

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Movimiento_Registrar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdUsuario", m.IdUsuario);
            cmd.Parameters.AddWithValue("@IdCategoria", m.IdCategoria);
            cmd.Parameters.AddWithValue("@IdTipoMovimiento", m.IdTipoMovimiento);
            cmd.Parameters.AddWithValue("@Monto", m.Monto);
            cmd.Parameters.AddWithValue("@Descripcion", (object)m.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaMovimiento", m.FechaMovimiento == default ? (object)DBNull.Value : m.FechaMovimiento);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Movimiento> Listar()
        {
            var resultado = new List<Movimiento>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Movimiento_Listar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                resultado.Add(new Movimiento
                {
                    IdMovimiento = reader.GetInt32(reader.GetOrdinal("IdMovimiento")),
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoria")),
                    IdTipoMovimiento = reader.GetInt32(reader.GetOrdinal("IdTipoMovimiento")),
                    Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    FechaMovimiento = reader.GetDateTime(reader.GetOrdinal("FechaMovimiento")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                    NombreUsuario = reader.GetString(reader.GetOrdinal("Usuario")),
                    NombreCategoria = reader.GetString(reader.GetOrdinal("Categoria")),
                    NombreTipoMovimiento = reader.GetString(reader.GetOrdinal("TipoMovimiento"))
                });
            }

            return resultado;
        }

        public Movimiento ObtenerPorId(int idMovimiento)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Movimiento_ObtenerPorId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMovimiento", idMovimiento);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Movimiento
                {
                    IdMovimiento = reader.GetInt32(reader.GetOrdinal("IdMovimiento")),
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoria")),
                    IdTipoMovimiento = reader.GetInt32(reader.GetOrdinal("IdTipoMovimiento")),
                    Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    FechaMovimiento = reader.GetDateTime(reader.GetOrdinal("FechaMovimiento")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                    NombreUsuario = reader.GetString(reader.GetOrdinal("Usuario")),
                    NombreCategoria = reader.GetString(reader.GetOrdinal("Categoria")),
                    NombreTipoMovimiento = reader.GetString(reader.GetOrdinal("TipoMovimiento"))
                };
            }

            throw new KeyNotFoundException("El movimiento no existe.");
        }

        public void Actualizar(Movimiento m)
        {
            if (m.Monto <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.");

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Movimiento_Editar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdMovimiento", m.IdMovimiento);
            cmd.Parameters.AddWithValue("@IdCategoria", m.IdCategoria);
            cmd.Parameters.AddWithValue("@IdTipoMovimiento", m.IdTipoMovimiento);
            cmd.Parameters.AddWithValue("@Monto", m.Monto);
            cmd.Parameters.AddWithValue("@Descripcion", (object)m.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaMovimiento", m.FechaMovimiento);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int idMovimiento)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Movimiento_Eliminar", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdMovimiento", idMovimiento);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}