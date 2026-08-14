namespace BilleteraDigital_Api.Models
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public int IdTipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public bool Estado { get; set; }

   
        public string? NombreUsuario { get; set; }
        public string? NombreCategoria { get; set; }
        public string? NombreTipoMovimiento { get; set; }
    }
}