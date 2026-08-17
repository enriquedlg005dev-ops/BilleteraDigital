using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioPorId(int id);
        string Registrar(UsuarioRequestRegistrar obj);
        string Editar(UsuarioRequestActualizar obj);
        string Eliminar(int id);
        UsuarioResponse Login(UsuarioRequestLogin obj);
    }
}