using BilleteraDigital_Api.Models;
using BilleteraDigital_Api.DTOs;
namespace BilleteraDigital_Api.Interfaces
{
    public interface ICategoriaService
    {
        void Insertar(Categoria categoria);
        List<Categoria> Listar();
        Categoria ObtenerPorId(int idCategoria);
        void Actualizar(Categoria c);
        void Eliminar(int idCategoria);
    }
}