using BilleteraDigital_Api.DTOs;
using BilleteraDigital_Api.Models;

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