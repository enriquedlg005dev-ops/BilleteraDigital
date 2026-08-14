namespace BilleteraDigital_Api.DTOs
{
    public class MovimientoRequest
    {

        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public int IdTipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }

    }
}
