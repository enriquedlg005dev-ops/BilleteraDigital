using System.ComponentModel.DataAnnotations;

namespace BilleteraDigital_Web.Models
{
    public class PresupuestoModel
    {
        public int IdPresupuesto { get; set; }

        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        public int IdCategoria { get; set; }
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "El monto límite es obligatorio")]
        [Range(0.01, 999999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
        [Display(Name = "Monto Límite")]
        public decimal MontoLimite { get; set; }

        [Display(Name = "Monto Gastado")]
        public decimal MontoGastado { get; set; }

        [Display(Name = "Monto Disponible")]
        public decimal MontoDisponible { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; } = DateTime.Now.AddDays(30);

        public bool Estado { get; set; }
    }
}