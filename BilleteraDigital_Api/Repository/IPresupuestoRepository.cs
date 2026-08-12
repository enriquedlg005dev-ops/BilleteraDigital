using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Repository
{
    public interface IPresupuestoRepository
    {
        List<PresupuestoDto> ListarPorUsuario(int idUsuario);
        PresupuestoDto ObtenerPorId(int idPresupuesto);
        bool Registrar(PresupuestoDto dto);
        bool Editar(PresupuestoDto dto);
        bool EliminarLogico(int idPresupuesto);
        DashboardResumenDto ObtenerResumenDashboard(int idUsuario);
        DashboardResumenDto ObtenerDashboard(int idUsuario);
    }
}