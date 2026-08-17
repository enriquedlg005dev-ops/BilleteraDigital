namespace BilleteraDigital_Web.Models
{
    public class PresupuestoViewModel
    {

        public decimal TotalPresupuestado { get; set; }
        public decimal TotalGastado { get; set; }
        public decimal Disponible => TotalPresupuestado - TotalGastado;

        public List<CategoriaResponse> ListaCategorias { get; set; } = new List<CategoriaResponse>();

        public List<PresupuestoItemDto> ListaPresupuestos { get; set; } = new List<PresupuestoItemDto>();
    }

    public class PresupuestoItemDto
    {
        public int IdPresupuesto { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public decimal MontoLimite { get; set; }
        public decimal MontoGastado { get; set; }
        public decimal Disponible => MontoLimite - MontoGastado;
        public double PorcentajeUtilizado => MontoLimite > 0 ? (double)(MontoGastado / MontoLimite) * 100 : 0;
        public DateTime FechaInicio { get; set; }
        public string MesAnio => FechaInicio.ToString("MMMM yyyy");
    }
}