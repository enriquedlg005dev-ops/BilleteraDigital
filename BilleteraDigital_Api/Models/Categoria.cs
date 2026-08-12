namespace BilleteraDigital_Api.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public int? IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
