using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.DTOs
{
    public class PresupuestoRequest
    {
        public string  IdUsuario { get; set; }
        public string  IdCategoria { get; set; }
        public string  MontoLimite { get; set; }
        public string  MontoGastado { get; set; }
        public string  FechaInicio { get; set; }
        public string  FechaFin { get; set; }
    }
}
