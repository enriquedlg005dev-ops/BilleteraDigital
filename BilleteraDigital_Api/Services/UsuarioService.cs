using System.Data;
using Microsoft.Data.SqlClient;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Repository;

namespace BilleteraDigital_Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string? _cadenaSql;

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
                            IdUsuario = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Apellido = dr.GetString(2),
                            Correo = dr.GetString(3),
                            Telefono = dr.GetString(4),
                            Estado = dr.GetBoolean(5),
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
                            IdUsuario = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Apellido = dr.GetString(2),
                            Correo = dr.GetString(3),
                            Telefono = dr.GetString(4),
                            Estado = dr.GetBoolean(5),
                            FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                        };
                    }
                }
            }
            return obj;
        }

        public string Registrar(UsuarioRequestRegistrar obj)
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

        public string Editar(UsuarioRequestActualizar obj)
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
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
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

        public UsuarioResponse Login(UsuarioRequestLogin obj)
        {
            UsuarioResponse usuario = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(_cadenaSql))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_Usuario_Login", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioResponse
                            {
                                IdUsuario = dr.GetInt32(0),
                                Nombre = dr.GetString(1),
                                Apellido = dr.GetString(2),
                                Correo = dr.GetString(3),
                                Telefono = dr.IsDBNull(4) ? null : dr.GetString(4)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // En un entorno real aquí se guarda el log del error
                throw new Exception("Error al intentar iniciar sesión: " + ex.Message);
            }

            return usuario; // Si las credenciales son malas, devolverá null
        }
    }
}