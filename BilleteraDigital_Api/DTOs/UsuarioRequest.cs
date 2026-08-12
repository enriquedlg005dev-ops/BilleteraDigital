namespace BilleteraDigital_Api.DTOs
{
    public class UsuarioRequest
    {

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public string? Telefono { get; set; }
    }
}
