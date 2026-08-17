using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Interfaces
{
    public interface IPresupuestoService
    {
        Task<IEnumerable<PresupuestoDTO>> ObtenerTodosAsync();
        Task<PresupuestoDTO?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(PresupuestoDTO presupuesto);
        Task<bool> ActualizarAsync(PresupuestoDTO presupuesto);
        Task<bool> EliminarLogicoAsync(int id);
    }
}