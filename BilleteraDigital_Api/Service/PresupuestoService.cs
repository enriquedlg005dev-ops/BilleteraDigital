using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Repository;

namespace BilleteraDigital_Api.Service
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _repo;

        public PresupuestoService(IPresupuestoRepository repo)
        {
            _repo = repo;
        }

        public List<PresupuestoDto> ListarPorUsuario(int idUsuario) => _repo.ListarPorUsuario(idUsuario);
        public PresupuestoDto ObtenerPorId(int idPresupuesto) => _repo.ObtenerPorId(idPresupuesto);
        public bool Registrar(PresupuestoDto p) => _repo.Registrar(p);
        public bool Editar(PresupuestoDto p) => _repo.Editar(p);
        public bool EliminarLogico(int idPresupuesto) => _repo.EliminarLogico(idPresupuesto);
        public DashboardResumenDto ObtenerDashboard(int idUsuario) => _repo.ObtenerDashboard(idUsuario);
    }
}