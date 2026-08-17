namespace BilleteraDigital_Web.Models
{
    public class DashboardViewModel
    {
        public DashboardResumenViewModel Resumen { get; set; }
           = new DashboardResumenViewModel();

        public List<DashboardCategoriaViewModel> Categorias { get; set; }
            = new List<DashboardCategoriaViewModel>();

        public List<DashboardTipoViewModel> Tipos { get; set; }
            = new List<DashboardTipoViewModel>();
    }
}
