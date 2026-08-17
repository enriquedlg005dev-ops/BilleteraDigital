
using BilleteraDigital_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BilleteraDigital_Web.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string apiBase = "https://localhost:7170/";

        public MovimientoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiBase);
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Movimiento");
            var content = await response.Content.ReadAsStringAsync();
            List<MovimientoResponse> list = JsonConvert.DeserializeObject<List<MovimientoResponse>>(content) ?? new List<MovimientoResponse>();

            List<CategoriaResponse> listCategorias = await this.listCategorias();
            ViewBag.categorias = new SelectList(listCategorias, "IdCategoria", "Nombre");

            return View(list);
        }

        async Task<List<CategoriaResponse>> listCategorias()
        {
            var response = await _httpClient.GetAsync("api/Categoria");
            var content = await response.Content.ReadAsStringAsync();
            List<CategoriaResponse> list = JsonConvert.DeserializeObject<List<CategoriaResponse>>(content) ?? new List<CategoriaResponse>();
            return list;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<CategoriaResponse> listCategorias = await this.listCategorias();
            ViewBag.categorias = new SelectList(listCategorias, "IdCategoria", "Nombre");
            return View(new MovimientoRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovimientoRequest movimiento)
        {
            var json = JsonConvert.SerializeObject(movimiento);
            var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Movimiento", body);
            var content = await response.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<MensajeResponse>(content) ?? new MensajeResponse();
            TempData["message"] = respuesta.mensaje;
            return RedirectToAction("Index");
        }

        async Task<MovimientoResponse> getMovimiento(int idMovimiento)
        {
            var response = await _httpClient.GetAsync("api/Movimiento/" + idMovimiento);
            var content = await response.Content.ReadAsStringAsync();
            var movimiento = JsonConvert.DeserializeObject<MovimientoResponse>(content) ?? new MovimientoResponse();
            return movimiento;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int idMovimiento)
        {
            var movimiento = await getMovimiento(idMovimiento);
            if (movimiento.IdMovimiento == 0)
            {
                TempData["message"] = "No existe el movimiento";
                return RedirectToAction("Index");
            }

            List<CategoriaResponse> listCategorias = await this.listCategorias();
            ViewBag.categorias = new SelectList(listCategorias, "IdCategoria", "Nombre", movimiento.IdCategoria);

            var request = new MovimientoRequest
            {
                IdUsuario = movimiento.IdUsuario,
                IdCategoria = movimiento.IdCategoria,
                IdTipoMovimiento = movimiento.IdTipoMovimiento,
                Monto = movimiento.Monto,
                Descripcion = movimiento.Descripcion
            };
            ViewBag.IdMovimiento = idMovimiento;
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idMovimiento, MovimientoRequest movimiento)
        {
            var json = JsonConvert.SerializeObject(movimiento);
            var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Movimiento/" + idMovimiento, body);
            var content = await response.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<MensajeResponse>(content) ?? new MensajeResponse();
            TempData["message"] = respuesta.mensaje;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idMovimiento)
        {
            var response = await _httpClient.DeleteAsync("api/Movimiento/" + idMovimiento);
            var content = await response.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<MensajeResponse>(content) ?? new MensajeResponse();
            TempData["message"] = respuesta.mensaje;
            return RedirectToAction("Index");
        }
    }
}