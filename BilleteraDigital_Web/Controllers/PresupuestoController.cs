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
                // Cargar categorías
                var categorias =
                    await _httpClient.GetFromJsonAsync<List<CategoriaResponse>>(
                        "api/Categoria"
                    );

                if (categorias != null)
                {
                    model.ListaCategorias = categorias
                        .Where(c => c.Estado)
                        .ToList();
                }

                // Cargar presupuestos
                var presupuestos =
                    await _httpClient.GetFromJsonAsync<List<PresupuestoResponse>>(
                        "api/Presupuesto"
                    );

                if (presupuestos != null)
                {
                    model.ListaPresupuestos = presupuestos
                        .Where(p => p.Estado)
                        .Select(p => new PresupuestoItemDto
                        {
                            IdPresupuesto = p.IdPresupuesto,
                            IdUsuario = p.IdUsuario,
                            IdCategoria = p.IdCategoria,
                            NombreCategoria = p.Categoria,
                            MontoLimite = p.MontoLimite,
                            MontoGastado = p.MontoGastado,
                            FechaInicio = p.FechaInicio.ToDateTime(
                                TimeOnly.MinValue
                            ),
                            FechaFin = p.FechaFin.ToDateTime(
                                TimeOnly.MinValue
                            )
                        })
                        .ToList();

                    model.TotalPresupuestado =
                        model.ListaPresupuestos.Sum(p => p.MontoLimite);

                    model.TotalGastado =
                        model.ListaPresupuestos.Sum(p => p.MontoGastado);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error =
                    "No se pudieron obtener los datos: " + ex.Message;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(
            [FromBody] PresupuestoRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Los datos son obligatorios."
                    });
                }

                var response =
                    await _httpClient.PostAsJsonAsync(
                        "api/Presupuesto",
                        request
                    );

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new
                    {
                        success = true,
                        message =
                            "Presupuesto registrado correctamente."
                    });
                }

                var error =
                    await response.Content.ReadAsStringAsync();

                return BadRequest(new
                {
                    success = false,
                    message =
                        "La API no pudo registrar el presupuesto.",
                    detalle = error
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message =
                        "Error al comunicarse con la API.",
                    detalle = ex.Message
                });
            }
        }


        // =========================================================
        // ACTUALIZAR
        // =========================================================

        [HttpPut]
        public async Task<IActionResult> Actualizar(
            [FromBody] PresupuestoRequestUpdate request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Los datos son obligatorios."
                    });
                }

                var response =
                    await _httpClient.PutAsJsonAsync(
                        "api/Presupuesto",
                        request
                    );

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new
                    {
                        success = true,
                        message =
                            "Presupuesto actualizado correctamente."
                    });
                }

                var error =
                    await response.Content.ReadAsStringAsync();

                return BadRequest(new
                {
                    success = false,
                    message =
                        "No se pudo actualizar el presupuesto.",
                    detalle = error
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message =
                        "Error al comunicarse con la API.",
                    detalle = ex.Message
                });
            }
        }


        // =========================================================
        // ELIMINAR
        // =========================================================

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var response =
                    await _httpClient.DeleteAsync(
                        $"api/Presupuesto/{id}"
                    );

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new
                    {
                        success = true,
                        message =
                            "Presupuesto eliminado correctamente."
                    });
                }

                var error =
                    await response.Content.ReadAsStringAsync();

                return BadRequest(new
                {
                    success = false,
                    message =
                        "No se pudo eliminar el presupuesto.",
                    detalle = error
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message =
                        "Error al comunicarse con la API.",
                    detalle = ex.Message
                });
            }
        }
    }
}