namespace BilleteraDigital_Web.Models
{
    public class Presupuesto
    {
        public int IdPresupuesto { get; set; }

        public int IdUsuario { get; set; }

        public int IdCategoria { get; set; }

        public decimal MontoLimite { get; set; }

        public decimal MontoGastado { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}