using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BilleteraDigital_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("movimientos")]
        public async Task<ActionResult<List<ReporteMovimientoResponse>>> ObtenerMovimientos(
            [FromQuery] int idUsuario,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null,
            [FromQuery] int? idTipoMovimiento = null,
            [FromQuery] int? idCategoria = null)
        {
            var movimientos = await _reporteService.ObtenerMovimientosAsync(
                idUsuario,
                fechaInicio,
                fechaFin,
                idTipoMovimiento,
                idCategoria
            );

            return Ok(movimientos);
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<ReporteCategoriaResponse>>> ObtenerCategorias(
           int idUsuario)
        {
            var categorias =
                await _reporteService.ObtenerCategoriasAsync(idUsuario);

            return Ok(categorias);
        }
    }
}
