namespace BilleteraDigital_Api.ModelsDtoRequest
{
    public class MovimientoRequest
    {

        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public int IdTipoMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }

    }
}
