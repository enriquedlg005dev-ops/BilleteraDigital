using Microsoft.AspNetCore.Mvc;
using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.Repository;
using BilleteraDigital_Api.DTOs;

namespace BilleteraDigital_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // Declaración de la interfaz para la inyección de dependencias
        private readonly IUsuario _usuarioService;

        // Constructor que inyecta el servicio
        public UsuarioController(IUsuario usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Listar")]
        public IActionResult GetUsuarios()
        {
            var lista = _usuarioService.GetUsuarios();
            return Ok(lista);
        }

        [HttpGet("Obtener/{id}")]
        public IActionResult GetUsuarioPorId(int id)
        {
            var usuario = _usuarioService.GetUsuarioPorId(id);
            if (usuario == null) 
                return NotFound(new { mensaje = "Usuario no encontrado" });
                
            return Ok(usuario);
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] UsuarioRequest obj)
        {
            var mensaje = _usuarioService.Registrar(obj);
            return Ok(new { mensaje });
        }

        [HttpPut("Editar")]
        public IActionResult Editar([FromBody] Usuario obj)
        {
            var mensaje = _usuarioService.Editar(obj);
            return Ok(new { mensaje });
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var mensaje = _usuarioService.Eliminar(id);
            return Ok(new { mensaje });
        }
    }
}