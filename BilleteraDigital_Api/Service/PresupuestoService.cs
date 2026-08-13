using System.Collections.Generic;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Repository;

namespace BilleteraDigital_Api.Service
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _repository;

        public PresupuestoService(IPresupuestoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Presupuesto> ObtenerPorUsuario(int idUsuario)
        {
            return _repository.ObtenerPorUsuario(idUsuario);
        }

        public int Registrar(Presupuesto presupuesto)
        {
            return _repository.Agregar(presupuesto);
        }

        public bool Desactivar(int idPresupuesto)
        {
            return _repository.EliminarLogico(idPresupuesto);
        }
    }
}