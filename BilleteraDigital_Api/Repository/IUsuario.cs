using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Repository
{
    public interface IUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioPorId(int id);
        string Registrar(UsuarioRequest obj);
        string Editar(Usuario obj);
        string Eliminar(int id);
    }
}