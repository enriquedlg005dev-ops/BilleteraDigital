namespace BilleteraDigital_Web.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        // Nullable int porque en la BD acepta nulos para categorías genéricas
        public int? IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
