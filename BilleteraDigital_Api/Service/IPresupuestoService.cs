using System.Collections.Generic;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Service
{
    public interface IPresupuestoService
    {
        IEnumerable<Presupuesto> ObtenerPorUsuario(int idUsuario);
        int Registrar(Presupuesto presupuesto);
        bool Desactivar(int idPresupuesto);
    }
}