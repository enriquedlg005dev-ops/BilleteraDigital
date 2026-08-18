using Microsoft.AspNetCore.Mvc;
using BilleteraDigital_Web.Models;
using System.Net.Http.Json;

namespace BilleteraDigital_Web.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly HttpClient _httpClient;

        public PresupuestoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BilleteraApi");
        }

        public async Task<IActionResult> Index()
        {
            var model = new PresupuestoViewModel();

            try
            {
                // 1. Cargar Categorías reales desde la BD a través de la API
                var categorias = await _httpClient.GetFromJsonAsync<List<CategoriaResponse>>("api/Categoria");
                if (categorias != null)
                {
                    model.ListaCategorias = categorias;
                }

                // 2. Cargar Presupuestos registrados desde la BD
                var presupuestos = await _httpClient.GetFromJsonAsync<List<PresupuestoItemDto>>("api/Presupuesto");
                if (presupuestos != null)
                {
                    model.ListaPresupuestos = presupuestos;
                    model.TotalPresupuestado = presupuestos.Sum(p => p.MontoLimite);
                    model.TotalGastado = presupuestos.Sum(p => p.MontoGastado);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudieron obtener los datos: " + ex.Message;
            }

            return View(model);
        }
    }
}