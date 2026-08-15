
using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using BilleteraDigital_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Asp_Web_Api_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _service;

        public MovimientoController(IMovimientoService service)
        {
            _service = service;
        }

        // Convierte el Model interno a lo que se muestra al usuario
        private static MovimientoResponse ToResponse(Movimiento m)
        {
            return new MovimientoResponse
            {
                IdMovimiento = m.IdMovimiento,
                IdUsuario = m.IdUsuario,
                NombreUsuario = m.NombreUsuario,
                IdCategoria = m.IdCategoria,
                NombreCategoria = m.NombreCategoria,
                IdTipoMovimiento = m.IdTipoMovimiento,
                NombreTipoMovimiento = m.NombreTipoMovimiento,
                Monto = m.Monto,
                Descripcion = m.Descripcion,
                FechaMovimiento = m.FechaMovimiento,
                Estado = m.Estado
            };
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var movimientos = _service.Listar();
            var response = movimientos.Select(ToResponse).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var movimiento = _service.ObtenerPorId(id);
                return Ok(ToResponse(movimiento));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Crear([FromBody] MovimientoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimiento = new Movimiento
            {
                IdUsuario = request.IdUsuario,
                IdCategoria = request.IdCategoria,
                IdTipoMovimiento = request.IdTipoMovimiento,
                Monto = request.Monto,
                Descripcion = request.Descripcion
            };

            try
            {
                _service.Insertar(movimiento);
                return StatusCode(201, new { mensaje = "Movimiento registrado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] MovimientoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimiento = new Movimiento
            {
                IdMovimiento = id,
                IdUsuario = request.IdUsuario,
                IdCategoria = request.IdCategoria,
                IdTipoMovimiento = request.IdTipoMovimiento,
                Monto = request.Monto,
                Descripcion = request.Descripcion,
                FechaMovimiento = DateTime.Now
            };

            try
            {
                _service.Actualizar(movimiento);
                return Ok(new { mensaje = "Movimiento actualizado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _service.Eliminar(id);
                return Ok(new { mensaje = "Movimiento eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}