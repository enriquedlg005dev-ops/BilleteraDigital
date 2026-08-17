using BilleteraDigital_Api.Models;
namespace BilleteraDigital_Api.Interfaces
{
    public interface IMovimientoService
    {
        void Insertar(Movimiento movimiento);
        List<Movimiento> Listar();
        Movimiento ObtenerPorId(int idMovimiento);
        void Actualizar(Movimiento movimiento);
        void Eliminar(int idMovimiento);
    }
}