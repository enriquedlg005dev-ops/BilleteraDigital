using BilleteraDigital_Api.DTOs;

namespace BilleteraDigital_Api.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResumenResponse> ObtenerResumenAsync(int idUsuario);

        Task<IEnumerable<DashboardCategoriaResponse>>
            ObtenerResumenPorCategoriaAsync(int idUsuario);

        Task<IEnumerable<DashboardTipoResponse>>
            ObtenerResumenPorTipoAsync(int idUsuario);
    }
}
