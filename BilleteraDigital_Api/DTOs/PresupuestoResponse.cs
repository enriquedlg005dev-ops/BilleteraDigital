namespace BilleteraDigital_Api.DTOs
{
    public class PresupuestoResponse
    {
        public int IdPresupuesto { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; } 
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public decimal MontoLimite { get; set; }
        public decimal MontoGastado { get; set; }
        public decimal MontoDisponible { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}