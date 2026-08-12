using Microsoft.AspNetCore.Mvc;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Service;

namespace BilleteraDigital_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _service;

        public PresupuestoController(IPresupuestoService service)
        {
            _service = service;
        }

        // GET (Obtener lista por usuario)
        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ListarPorUsuario(int idUsuario) => Ok(_service.ListarPorUsuario(idUsuario));

        // GET (Obtener por ID)
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id) => Ok(_service.ObtenerPorId(id));

        // POST (Crear)
        [HttpPost]
        public IActionResult Registrar([FromBody] PresupuestoDto p)
        {
            bool res = _service.Registrar(p);
            return res ? Ok(new { mensaje = "Registrado correctamente" }) : BadRequest();
        }

        // PUT (Actualizar)
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] PresupuestoDto p)
        {
            p.IdPresupuesto = id;
            bool res = _service.Editar(p);
            return res ? Ok(new { mensaje = "Actualizado correctamente" }) : BadRequest();
        }

        // DELETE (Eliminación Lógica)
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            bool res = _service.EliminarLogico(id);
            return res ? Ok(new { mensaje = "Desactivado correctamente" }) : BadRequest();
        }

        // GET (Dashboard con totales de ingreso y gasto)
        [HttpGet("dashboard/{idUsuario}")]
        public IActionResult Dashboard(int idUsuario) => Ok(_service.ObtenerDashboard(idUsuario));
    }
}