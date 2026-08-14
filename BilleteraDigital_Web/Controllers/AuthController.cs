using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using BilleteraDigital_Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BilleteraDigital_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string apiBase = "https://localhost:7170/"; // Asegúrate de que este sea el puerto de tu API

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiBase);
        }

        // --- GET Y POST PARA LOGIN ---
        [HttpGet]
        public IActionResult Login()
        {
            // Si el usuario ya está logueado, lo mandamos directo al inicio
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            var json = JsonConvert.SerializeObject(modelo);
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Usuario/Login", body);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Usamos un dynamic (o puedes crear un UsuarioResponseViewModel) para leer la respuesta
                dynamic usuario = JsonConvert.DeserializeObject(content)!;

                // 1. Creamos la "identidad" del usuario (sus datos en memoria)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.nombre.ToString()),
                    new Claim(ClaimTypes.Email, usuario.correo.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 2. Creamos la sesión en el navegador (Cookie)
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            // Si el API devuelve error (401 Unauthorized, etc.)
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(modelo);
        }

        // --- GET Y POST PARA REGISTRO ---
        [HttpGet]
        public IActionResult Registro()
        {
            return View(new RegistroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            var json = JsonConvert.SerializeObject(modelo);
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Usuario/Registrar", body);

            if (response.IsSuccessStatusCode)
            {
                // Registro exitoso, lo mandamos a que inicie sesión
                TempData["message"] = "Cuenta creada exitosamente. Por favor, inicia sesión.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario.");
            return View(modelo);
        }

        // --- CERRAR SESIÓN ---
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}