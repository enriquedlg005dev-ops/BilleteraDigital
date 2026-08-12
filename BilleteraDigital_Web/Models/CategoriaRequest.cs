namespace BilleteraDigital_Web.Models
{
    public class CategoriaRequest

    {
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
