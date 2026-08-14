namespace BilleteraDigital_Api.DTOs
{
    public class UsuarioRequestLogin
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}