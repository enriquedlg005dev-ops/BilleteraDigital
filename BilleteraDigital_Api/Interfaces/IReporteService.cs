using BilleteraDigital_Api.DTOs;

namespace BilleteraDigital_Api.Interfaces
{
    public interface IReporteService
    {
        Task<List<ReporteMovimientoResponse>> ObtenerMovimientosAsync(
           int idUsuario,
           DateTime? fechaInicio = null,
           DateTime? fechaFin = null,
           int? idTipoMovimiento = null,
           int? idCategoria = null);

        Task<List<ReporteCategoriaResponse>> ObtenerCategoriasAsync(
     int idUsuario);
    }
}
