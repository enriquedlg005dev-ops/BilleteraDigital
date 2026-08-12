namespace BilleteraDigital_Api.Models
{
    public class PresupuestoDto
    {
        public int IdPresupuesto { get; set; }
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public int IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public decimal MontoLimite { get; set; }
        public decimal MontoGastado { get; set; }
        public decimal MontoDisponible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
    }

    public class PresupuestoCrearDto
    {
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public decimal MontoLimite { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class PresupuestoEditarDto
    {
        public int IdPresupuesto { get; set; }
        public int IdCategoria { get; set; }
        public decimal MontoLimite { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    // DTO para el Dashboard (Totales de Ingreso, Gasto y Saldo)
    public class DashboardResumenDto
    {
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal SaldoTotal { get; set; }
        public int CantidadPresupuestosActivos { get; set; }
    }
}