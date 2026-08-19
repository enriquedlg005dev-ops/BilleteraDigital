namespace BilleteraDigital_Api.DTOs
{
    public class PresupuestoResponse
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime InicioPresupuesto { get; set; }
        public DateTime FinalPresupuesto { get; set; }
        public int UsuarioId { get; set; }
        public int Estado { get; set; }
    }
}