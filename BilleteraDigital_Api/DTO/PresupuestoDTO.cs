namespace BilleteraDigital_Api.DTOs
{
    public class PresupuestoDTO
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public int Estado { get; set; } 
    }
}