using System.Collections.Generic;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Repository
{
    public interface IPresupuestoRepository
    {
        IEnumerable<Presupuesto> ObtenerPorUsuario(int idUsuario);
        int Agregar(Presupuesto presupuesto);
        bool EliminarLogico(int idPresupuesto);

    }
    
}