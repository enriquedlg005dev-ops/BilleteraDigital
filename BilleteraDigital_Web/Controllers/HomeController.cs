using System.Diagnostics;
using BilleteraDigital_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BilleteraDigital_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BilleteraApi");
        }

        public async Task<IActionResult> Index()
        {
            int idUsuario = 1;

            var resumen = await _httpClient.GetFromJsonAsync<DashboardResumenViewModel>(
                $"api/Dashboard/resumen/{idUsuario}");

            var categorias = await _httpClient.GetFromJsonAsync<List<DashboardCategoriaViewModel>>(
                $"api/Dashboard/categorias/{idUsuario}");

            var tipos = await _httpClient.GetFromJsonAsync<List<DashboardTipoViewModel>>(
                $"api/Dashboard/tipos/{idUsuario}");

            var model = new DashboardViewModel
            {
                Resumen = resumen ?? new DashboardResumenViewModel(),
                Categorias = categorias ?? new List<DashboardCategoriaViewModel>(),
                Tipos = tipos ?? new List<DashboardTipoViewModel>()
            };

            return View(model);
        }
    }
}
