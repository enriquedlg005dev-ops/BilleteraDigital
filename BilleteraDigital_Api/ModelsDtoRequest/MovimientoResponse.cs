namespace BilleteraDigital_Api.ModelsDtoRequest
{
    public class MovimientoResponse
    {


        public int IdMovimiento { get; set; }
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public int IdTipoMovimiento { get; set; }
        public string? NombreTipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public bool Estado { get; set; }
    }
}
