using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BilleteraDigital_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
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
        public IActionResult Registrar([FromBody] UsuarioRequestRegistrar obj)
        {
            var mensaje = _usuarioService.Registrar(obj);
            return Ok(new { mensaje });
        }

        [HttpPut("Editar")]
        public IActionResult Editar([FromBody] UsuarioRequestActualizar obj)
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

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UsuarioRequestLogin obj)
        {
            var usuario = _usuarioService.Login(obj);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos" });
            }

            return Ok(usuario);
        }
    }
}