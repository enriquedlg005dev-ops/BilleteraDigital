namespace BilleteraDigital_Web.Models
{
    public class PresupuestoRequestWeb
    {

        public string IdUsuario { get; set; } = string.Empty;

        public string IdCategoria { get; set; } = string.Empty;

        public string MontoLimite { get; set; } = string.Empty;

        public string MontoGastado { get; set; } = string.Empty;

        public string FechaInicio { get; set; } = string.Empty;

        public string FechaFin { get; set; } = string.Empty;
    }
}
