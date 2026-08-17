using BilleteraDigital_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BilleteraDigital_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("resumen/{idUsuario}")]
        public async Task<IActionResult> ObtenerResumen(int idUsuario)
        {
            var resultado =
                await _service.ObtenerResumenAsync(idUsuario);

            return Ok(resultado);
        }

        [HttpGet("categorias/{idUsuario}")]
        public async Task<IActionResult> ObtenerCategorias(int idUsuario)
        {
            var resultado =
                await _service.ObtenerResumenPorCategoriaAsync(idUsuario);

            return Ok(resultado);
        }

        [HttpGet("tipos/{idUsuario}")]
        public async Task<IActionResult> ObtenerTipos(int idUsuario)
        {
            var resultado =
                await _service.ObtenerResumenPorTipoAsync(idUsuario);

            return Ok(resultado);
        }
    }
}
