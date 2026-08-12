using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Service
{
    public interface IPresupuestoService
    {
        List<PresupuestoDto> ListarPorUsuario(int idUsuario);
        PresupuestoDto ObtenerPorId(int idPresupuesto);
        bool Registrar(PresupuestoDto p);
        bool Editar(PresupuestoDto p);
        bool EliminarLogico(int idPresupuesto);
        DashboardResumenDto ObtenerDashboard(int idUsuario);
    }
}