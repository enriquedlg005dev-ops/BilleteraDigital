
using BilleteraDigital_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BilleteraDigital_Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string apiBase = "https://localhost:7170/";

        public CategoriaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiBase);
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Categoria");
            var content = await response.Content.ReadAsStringAsync();
            List<CategoriaResponse> list = JsonConvert.DeserializeObject<List<CategoriaResponse>>(content) ?? new List<CategoriaResponse>();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CategoriaRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaRequest categoria)
        {
            var json = JsonConvert.SerializeObject(categoria);
            var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Categoria", body);
            var content = await response.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<MensajeResponse>(content) ?? new MensajeResponse();
            TempData["message"] = respuesta.mensaje;
            return RedirectToAction("Index");
        }

        async Task<CategoriaResponse> getCategoria(int idCategoria)
        {
            var response = await _httpClient.GetAsync("api/Categoria/" + idCategoria);
            var content = await response.Content.ReadAsStringAsync();
            var categoria = JsonConvert.DeserializeObject<CategoriaResponse>(content) ?? new CategoriaResponse();
            return categoria;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int idCategoria)
        {
            var categoria = await getCategoria(idCategoria);
            if (categoria.IdCategoria == 0)
            {
                TempData["message"] = "No existe la categoria";
                return RedirectToAction("Index");
            }

            var request = new CategoriaRequest
            {
                IdUsuario = categoria.IdUsuario,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
            ViewBag.IdCategoria = idCategoria;
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idCategoria, CategoriaRequest categoria)
        {
            var json = JsonConvert.SerializeObject(categoria);
            var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Categoria/" + idCategoria, body);
            var content = await response.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<MensajeResponse>(content) ?? new MensajeResponse();
            TempData["message"] = respuesta.mensaje;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idCategoria)
        {
            var response = await _httpClient.DeleteAsync("api/Categoria/" + idCategoria);
            var content = await response.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<MensajeResponse>(content) ?? new MensajeResponse();
            TempData["message"] = respuesta.mensaje;
            return RedirectToAction("Index");
        }
    }
}