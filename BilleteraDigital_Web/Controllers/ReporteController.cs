using BilleteraDigital_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilleteraDigital_Web.Controllers
{
    public class ReporteController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReporteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BilleteraApi");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(
            int idUsuario,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? idTipoMovimiento,
            int? idCategoria)
        {
            var url =
                $"api/Reporte/movimientos?idUsuario={idUsuario}";

            if (fechaInicio.HasValue)
            {
                url +=
                    $"&fechaInicio={fechaInicio.Value:yyyy-MM-dd}";
            }

            if (fechaFin.HasValue)
            {
                url +=
                    $"&fechaFin={fechaFin.Value:yyyy-MM-dd}";
            }

            if (idTipoMovimiento.HasValue)
            {
                url +=
                    $"&idTipoMovimiento={idTipoMovimiento.Value}";
            }

            if (idCategoria.HasValue)
            {
                url +=
                    $"&idCategoria={idCategoria.Value}";
            }

            var movimientos =
                await _httpClient.GetFromJsonAsync<
                    List<ReporteMovimientoModel>>(url);

            return Json(movimientos);
        }

        [HttpGet]
        public async Task<IActionResult> Categorias(int idUsuario)
        {
            var categorias =
                await _httpClient.GetFromJsonAsync<
                    List<ReporteCategoriaModel>>(
                        $"api/Reporte/categorias?idUsuario={idUsuario}");

            return Json(categorias);
        }
    }
}
