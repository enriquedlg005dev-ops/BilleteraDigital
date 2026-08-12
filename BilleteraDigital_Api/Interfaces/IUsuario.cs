using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioPorId(int id);
        string Registrar(Usuario obj);
        string Editar(Usuario obj);
        string Eliminar(int id);
    }
}