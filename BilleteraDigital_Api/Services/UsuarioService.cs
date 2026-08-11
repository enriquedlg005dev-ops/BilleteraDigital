using System.Data;
using Microsoft.Data.SqlClient;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Repository;

namespace BilleteraDigital_Api.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly string _cadenaSql;

        public UsuarioService(IConfiguration config)
        {
            // Asegúrate de que el nombre coincida con tu appsettings.json
            _cadenaSql = config.GetConnectionString("DefaultConnection"); 
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection cn = new SqlConnection(_cadenaSql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Usuario_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : null,
                            Estado = Convert.ToBoolean(dr["Estado"]),
                            FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                        });
                    }
                }
            }
            return lista;
        }

        public Usuario GetUsuarioPorId(int id)
        {
            Usuario obj = null;

            using (SqlConnection cn = new SqlConnection(_cadenaSql))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_Usuario_ObtenerPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        obj = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : null,
                            Estado = Convert.ToBoolean(dr["Estado"]),
                            FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                        };
                    }
                }
            }
            return obj;
        }

        public string Registrar(Usuario obj)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaSql))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Registrar", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(obj.Telefono) ? DBNull.Value : obj.Telefono);

                    // El procedimiento devuelve un SCOPE_IDENTITY()
                    object result = cmd.ExecuteScalar(); 
                    mensaje = $"Usuario registrado correctamente con ID: {result}";
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
            return mensaje;
        }

        public string Editar(Usuario obj)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaSql))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Editar", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(obj.Telefono) ? DBNull.Value : obj.Telefono);

                    // Leemos el mensaje devuelto por el procedimiento almacenado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            mensaje = dr["Mensaje"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
            return mensaje;
        }

        public string Eliminar(int id)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaSql))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Eliminar", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@IdUsuario", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            mensaje = dr["Mensaje"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
            return mensaje;
        }
    }
}