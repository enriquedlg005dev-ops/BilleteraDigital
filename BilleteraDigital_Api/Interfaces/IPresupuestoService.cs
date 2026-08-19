using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Interfaces
{
    public interface IPresupuestoService
    {
        Task<IEnumerable<PresupuestoResponse>> ObtenerTodosAsync();
        Task<PresupuestoResponse?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(PresupuestoRequest presupuesto);
        Task<bool> ActualizarAsync(PresupuestoRequestUpdate presupuesto);
        Task<bool> EliminarLogicoAsync(int id);
    }
}