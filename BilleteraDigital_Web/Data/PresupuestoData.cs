using System.Data;
using Microsoft.Data.SqlClient;
using BilleteraDigital_Web.Models;

namespace BilleteraDigital_Web.Data
{
    public class PresupuestoData
    {
        private readonly string _cadenaSQL;

        public PresupuestoData(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL")!;
        }

        public List<PresupuestoModel> Listar(int idUsuario)
        {
            var lista = new List<PresupuestoModel>();

            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PresupuestoModel
                        {
                            IdPresupuesto = Convert.ToInt32(dr["IdPresupuesto"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Usuario = dr["Usuario"].ToString(),
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Categoria = dr["Categoria"].ToString(),
                            MontoLimite = Convert.ToDecimal(dr["MontoLimite"]),
                            MontoGastado = Convert.ToDecimal(dr["MontoGastado"]),
                            MontoDisponible = Convert.ToDecimal(dr["MontoDisponible"]),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public PresupuestoModel ObtenerPorId(int idPresupuesto)
        {
            var presupuesto = new PresupuestoModel();

            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_ObtenerPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPresupuesto", idPresupuesto);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        presupuesto.IdPresupuesto = Convert.ToInt32(dr["IdPresupuesto"]);
                        presupuesto.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        presupuesto.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                        presupuesto.MontoLimite = Convert.ToDecimal(dr["MontoLimite"]);
                        presupuesto.MontoGastado = Convert.ToDecimal(dr["MontoGastado"]);
                        presupuesto.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);
                        presupuesto.FechaFin = Convert.ToDateTime(dr["FechaFin"]);
                        presupuesto.Estado = Convert.ToBoolean(dr["Estado"]);
                    }
                }
            }
            return presupuesto;
        }

        public bool Registrar(PresupuestoModel model)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_Registrar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", model.IdUsuario);
                cmd.Parameters.AddWithValue("@IdCategoria", model.IdCategoria);
                cmd.Parameters.AddWithValue("@MontoLimite", model.MontoLimite);
                cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Editar(PresupuestoModel model)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_Editar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPresupuesto", model.IdPresupuesto);
                cmd.Parameters.AddWithValue("@IdCategoria", model.IdCategoria);
                cmd.Parameters.AddWithValue("@MontoLimite", model.MontoLimite);
                cmd.Parameters.AddWithValue("@FechaInicio", model.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", model.FechaFin);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idPresupuesto)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_Eliminar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPresupuesto", idPresupuesto);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Categoria> ListarCategorias()
        {
            var lista = new List<Categoria>();

            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Categoria_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categoria
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Nombre = dr["Nombre"].ToString()!
                        });
                    }
                }
            }
            return lista;
        }
    }
}