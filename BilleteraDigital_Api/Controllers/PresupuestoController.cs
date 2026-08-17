using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Service;


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

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult GetByUsuario(int idUsuario)
        {
            var result = _service.ObtenerPorUsuario(idUsuario);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Presupuesto presupuesto)
        {
            if (presupuesto == null || presupuesto.MontoLimite <= 0)
            {
                return BadRequest(new { mensaje = "Monto no válido." });
            }

            var id = _service.Registrar(presupuesto);
            return Ok(new { mensaje = "Registrado correctamente", id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exito = _service.Desactivar(id);
            if (!exito) return NotFound(new { mensaje = "No se encontró el registro." });

            return Ok(new { mensaje = "Presupuesto desactivado correctamente." });
        }
    }
}