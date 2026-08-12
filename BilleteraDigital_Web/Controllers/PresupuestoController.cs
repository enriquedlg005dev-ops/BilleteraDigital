using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BilleteraDigital_Web.Data;
using BilleteraDigital_Web.Models;

namespace BilleteraDigital_Web.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly PresupuestoData _presupuestoData;

        public PresupuestoController(PresupuestoData presupuestoData)
        {
            _presupuestoData = presupuestoData;
        }

        private int? ObtenerUsuarioSesion()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            return idUsuario ?? 1; // Usamos 1 por defecto mientras integras el Login
        }

        private void CargarCategorias()
        {
            ViewBag.Categorias = new SelectList(_presupuestoData.ListarCategorias(), "IdCategoria", "Nombre");
        }

        
        public IActionResult Index()
        {
            int idUsuario = ObtenerUsuarioSesion().Value;
            var lista = _presupuestoData.Listar(idUsuario);
            return View(lista);
        }

       
        public IActionResult Crear()
        {
            CargarCategorias();
            return View(new PresupuestoModel());
        }

        
        [HttpPost]
        public IActionResult Crear(PresupuestoModel model)
        {
            model.IdUsuario = ObtenerUsuarioSesion().Value;

            if (model.FechaFin < model.FechaInicio)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin no puede ser menor a la fecha de inicio.");
            }

            if (ModelState.IsValid)
            {
                bool respuesta = _presupuestoData.Registrar(model);
                if (respuesta)
                    return RedirectToAction(nameof(Index));
            }

            CargarCategorias();
            return View(model);
        }

        
        public IActionResult Editar(int id)
        {
            var model = _presupuestoData.ObtenerPorId(id);
            if (model.IdPresupuesto == 0)
                return NotFound();

            CargarCategorias();
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Editar(PresupuestoModel model)
        {
            if (model.FechaFin < model.FechaInicio)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin no puede ser menor a la fecha de inicio.");
            }

            if (ModelState.IsValid)
            {
                bool respuesta = _presupuestoData.Editar(model);
                if (respuesta)
                    return RedirectToAction(nameof(Index));
            }

            CargarCategorias();
            return View(model);
        }

        
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _presupuestoData.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}