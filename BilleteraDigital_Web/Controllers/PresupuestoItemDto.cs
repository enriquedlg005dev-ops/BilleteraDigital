using Microsoft.AspNetCore.Mvc;
using BilleteraDigital_Web.Models;
using System.Collections.Generic;
using System;

namespace BilleteraDigital_Web.Controllers
{
    public class PresupuestoController : Controller
    {
        public IActionResult Index()
        {
            var model = new PresupuestoViewModel
            {
                TotalPresupuestado = 4560.00m,
                TotalGastado = 3595.00m,
                ListaCategorias = new List<Categoria>
                {
                    new Categoria { IdCategoria = 1, Nombre = "Comida" },
                    new Categoria { IdCategoria = 2, Nombre = "Componentes PC" },
                    new Categoria { IdCategoria = 3, Nombre = "Servicios" }
                },
                ListaPresupuestos = new List<PresupuestoItemDto>
                {
                    new PresupuestoItemDto
                    {
                        IdPresupuesto = 1,
                        IdCategoria = 1,
                        NombreCategoria = "Comida",
                        MontoLimite = 560.00m,
                        MontoGastado = 35.00m,
                        FechaInicio = new DateTime(2026, 7, 1)
                    },
                    new PresupuestoItemDto
                    {
                        IdPresupuesto = 2,
                        IdCategoria = 2,
                        NombreCategoria = "Componentes PC",
                        MontoLimite = 4000.00m,
                        MontoGastado = 3560.00m,
                        FechaInicio = new DateTime(2026, 6, 1)
                    }
                }
            };

            return View(model);
        }
    }
}