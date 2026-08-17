using System.Data;
using Microsoft.Data.SqlClient;
using BilleteraDigital_Api.Interfaces;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.DTOs;

namespace Asp_Web_Api_.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly string _connectionString;

        public CategoriaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Insertar(Categoria c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Categoria_Registrar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdUsuario", (object)c.IdUsuario ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", (object)c.Descripcion ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Categoria> Listar()
        {
            var resultado = new List<Categoria>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Categoria_Listar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                resultado.Add(new Categoria
                {
                    IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoria")),
                    IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? null : reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))
                });
            }

            return resultado;
        }

        public Categoria ObtenerPorId(int idCategoria)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Categoria_ObtenerPorId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Categoria
                {
                    IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoria")),
                    IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? null : reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))
                };
            }

            throw new KeyNotFoundException("La categoría no existe.");
        }

        public void Actualizar(Categoria c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Categoria_Editar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdCategoria", c.IdCategoria);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", (object)c.Descripcion ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int idCategoria)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Categoria_Eliminar", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        
    }
}