
using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using BilleteraDigital_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Asp_Web_Api_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        private static CategoriaResponse ToResponse(Categoria c)
        {
            return new CategoriaResponse
            {
                IdCategoria = c.IdCategoria,
                IdUsuario = c.IdUsuario,
                Nombre = c.Nombre ?? string.Empty,
                Descripcion = c.Descripcion,
                Estado = c.Estado,
                FechaRegistro = c.FechaRegistro
            };
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var categorias = _service.Listar();
            var response = categorias.Select(ToResponse).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var categoria = _service.ObtenerPorId(id);
                return Ok(ToResponse(categoria));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Crear([FromBody] CategoriaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = new Categoria
            {
                IdUsuario = request.IdUsuario,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            try
            {
                _service.Insertar(categoria);
                return StatusCode(201, new { mensaje = "Categoría creada correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] CategoriaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = new Categoria
            {
                IdCategoria = id,
                IdUsuario = request.IdUsuario,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            try
            {
                _service.Actualizar(categoria);
                return Ok(new { mensaje = "Categoría actualizada correctamente." });
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
                return Ok(new { mensaje = "Categoría eliminada correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}