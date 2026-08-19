namespace BilleteraDigital_Api.DTOs
{
    public class ReporteMovimientoResponse
    {
        public int IdMovimiento { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public string TipoMovimiento { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal Monto { get; set; }

        public string Usuario { get; set; } = string.Empty;
    }
}
