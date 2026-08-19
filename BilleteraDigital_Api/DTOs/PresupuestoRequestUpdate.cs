namespace BilleteraDigital_Api.DTOs
{
    public class PresupuestoRequestUpdate
    {
        public int IdPresupuesto { get; set; }
        public string IdCategoria { get; set; }
        public string MontoLimite { get; set; }
        public string MontoGastado { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
