using System.Data;
using Microsoft.Data.SqlClient;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Repository
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly string _cadenaSQL;

        public PresupuestoRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("conexion")!;
        }

        public List<PresupuestoDto> ListarPorUsuario(int idUsuario)
        {
            var lista = new List<PresupuestoDto>();

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
                        lista.Add(new PresupuestoDto
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

        public PresupuestoDto ObtenerPorId(int idPresupuesto)
        {
            var presupuesto = new PresupuestoDto();

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

        public bool Registrar(PresupuestoDto dto)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_Registrar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", dto.IdUsuario);
                cmd.Parameters.AddWithValue("@IdCategoria", dto.IdCategoria);
                cmd.Parameters.AddWithValue("@MontoLimite", dto.MontoLimite);
                cmd.Parameters.AddWithValue("@FechaInicio", dto.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", dto.FechaFin);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Editar(PresupuestoDto dto)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand("sp_Presupuesto_Editar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPresupuesto", dto.IdPresupuesto);
                cmd.Parameters.AddWithValue("@IdCategoria", dto.IdCategoria);
                cmd.Parameters.AddWithValue("@MontoLimite", dto.MontoLimite);
                cmd.Parameters.AddWithValue("@FechaInicio", dto.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", dto.FechaFin);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarLogico(int idPresupuesto)
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

        public DashboardResumenDto ObtenerResumenDashboard(int idUsuario)
        {
            var resumen = new DashboardResumenDto();

            using (SqlConnection cn = new SqlConnection(_cadenaSQL))
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        ISNULL((SELECT SUM(Monto) FROM Movimiento M 
                                INNER JOIN TipoMovimiento TM ON M.IdTipoMovimiento = TM.IdTipoMovimiento 
                                WHERE M.IdUsuario = @IdUsuario AND TM.Nombre = 'Ingreso' AND M.Estado = 1), 0) AS TotalIngresos,
                        
                        ISNULL((SELECT SUM(Monto) FROM Movimiento M 
                                INNER JOIN TipoMovimiento TM ON M.IdTipoMovimiento = TM.IdTipoMovimiento 
                                WHERE M.IdUsuario = @IdUsuario AND TM.Nombre = 'Gasto' AND M.Estado = 1), 0) AS TotalGastos,
                        
                        (SELECT COUNT(1) FROM Presupuesto WHERE IdUsuario = @IdUsuario AND Estado = 1) AS CantidadPresupuestosActivos", cn);

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        resumen.TotalIngresos = Convert.ToDecimal(dr["TotalIngresos"]);
                        resumen.TotalGastos = Convert.ToDecimal(dr["TotalGastos"]);
                        resumen.SaldoTotal = resumen.TotalIngresos - resumen.TotalGastos;
                        resumen.CantidadPresupuestosActivos = Convert.ToInt32(dr["CantidadPresupuestosActivos"]);
                    }
                }
            }
            return resumen;
        }
    }
}