namespace BilleteraDigital_Web.Models
{
    public class PresupuestoRequest
    {
        public string IdUsuario { get; set; } = string.Empty;

        public string IdCategoria { get; set; } = string.Empty;

        public string MontoLimite { get; set; } = string.Empty;

        public string MontoGastado { get; set; } = string.Empty;

        public string FechaInicio { get; set; } = string.Empty;

        public string FechaFin { get; set; } = string.Empty;
    }


    public class PresupuestoRequestUpdate
    {
        public int IdPresupuesto { get; set; }

        public string IdCategoria { get; set; } = string.Empty;

        public string MontoLimite { get; set; } = string.Empty;

        public string MontoGastado { get; set; } = string.Empty;

        public string FechaInicio { get; set; } = string.Empty;

        public string FechaFin { get; set; } = string.Empty;
    }


    public class PresupuestoResponse
    {
        public int IdPresupuesto { get; set; }

        public int IdUsuario { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public int IdCategoria { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public decimal MontoLimite { get; set; }

        public decimal MontoGastado { get; set; }

        public decimal MontoDisponible { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaFin { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
