namespace BilleteraDigital_Api.ModelsDtoRequest
{
    public class CategoriaResponse
    {
        public int IdCategoria { get; set; }
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
