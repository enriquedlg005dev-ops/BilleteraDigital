using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Text;
using BilleteraDigital_Web.Models;

namespace BilleteraDigital_Web.Controllers
{
    [Authorize] // Exige que el usuario haya iniciado sesión para entrar aquí
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string apiBase = "https://localhost:7170/"; // Recuerda poner tu puerto real

        public UsuarioController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiBase);
        }

        // GET: Muestra el formulario con los datos actuales
        [HttpGet]
        public async Task<IActionResult> MiPerfil()
        {
            // 1. Obtenemos el ID del usuario logueado desde su Cookie
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // 2. Consumimos tu API: [HttpGet("Obtener/{id}")]
            var response = await _httpClient.GetAsync($"api/Usuario/Obtener/{idClaim}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Convertimos la respuesta del API directo a nuestro ViewModel
                var perfil = JsonConvert.DeserializeObject<PerfilViewModel>(content);
                return View(perfil);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Envía los datos modificados a la API
        [HttpPost]
        public async Task<IActionResult> MiPerfil(PerfilViewModel modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            // Aseguramos que el ID sea el del usuario logueado, por seguridad
            modelo.IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var json = JsonConvert.SerializeObject(modelo);
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            // Consumimos tu API: [HttpPut("Editar")]
            var response = await _httpClient.PutAsync("api/Usuario/Editar", body);

            if (response.IsSuccessStatusCode)
            {
                TempData["MensajeExito"] = "Tus datos fueron actualizados correctamente.";
                return RedirectToAction("MiPerfil");
            }

            TempData["MensajeError"] = "Ocurrió un error al actualizar.";
            return View(modelo);
        }
    }
}