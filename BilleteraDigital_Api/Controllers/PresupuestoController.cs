using Microsoft.AspNetCore.Mvc;
using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;

namespace BilleteraDigital_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _service;

        public PresupuestoController(IPresupuestoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.ObtenerTodosAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.ObtenerPorIdAsync(id);

            if (result == null)
                return NotFound(new { mensaje = "No se encontró el presupuesto." });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PresupuestoDTO presupuesto)
        {
            if (presupuesto == null)
                return BadRequest(new { mensaje = "Los datos del presupuesto son obligatorios." });

            var exito = await _service.CrearAsync(presupuesto);

            if (!exito)
                return BadRequest(new { mensaje = "No se pudo registrar el presupuesto." });

            return Ok(new { mensaje = "Presupuesto registrado correctamente." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PresupuestoDTO presupuesto)
        {
            if (presupuesto == null)
                return BadRequest(new { mensaje = "Los datos del presupuesto son obligatorios." });

            var exito = await _service.ActualizarAsync(presupuesto);

            if (!exito)
                return NotFound(new { mensaje = "No se encontró el presupuesto." });

            return Ok(new { mensaje = "Presupuesto actualizado correctamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exito = await _service.EliminarLogicoAsync(id);

            if (!exito)
                return NotFound(new { mensaje = "No se encontró el presupuesto." });

            return Ok(new { mensaje = "Presupuesto eliminado correctamente." });
        }
    }
}