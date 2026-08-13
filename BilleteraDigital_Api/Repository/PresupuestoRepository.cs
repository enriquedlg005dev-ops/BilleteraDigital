using System;
using System.Collections.Generic;
using System.Linq;
using BilleteraDigital_Api.Models;

namespace BilleteraDigital_Api.Repository
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        // Simulación de datos en memoria para pruebas
        private static List<Presupuesto> _lista = new List<Presupuesto>
        {
            new Presupuesto
            {
                IdPresupuesto = 1, IdUsuario = 1, IdCategoria = 1,
                MontoLimite = 560.00m, MontoGastado = 35.00m,
                FechaInicio = new DateTime(2026, 7, 1), FechaFin = new DateTime(2026, 7, 31),
                Estado = true, FechaRegistro = DateTime.Now
            },
            new Presupuesto
            {
                IdPresupuesto = 2, IdUsuario = 1, IdCategoria = 2,
                MontoLimite = 4000.00m, MontoGastado = 3560.00m,
                FechaInicio = new DateTime(2026, 6, 1), FechaFin = new DateTime(2026, 6, 30),
                Estado = true, FechaRegistro = DateTime.Now
            }
        };

        public IEnumerable<Presupuesto> ObtenerPorUsuario(int idUsuario)
        {
            return _lista.Where(p => p.IdUsuario == idUsuario && p.Estado);
        }

        public int Agregar(Presupuesto presupuesto)
        {
            presupuesto.IdPresupuesto = _lista.Count + 1;
            presupuesto.Estado = true;
            presupuesto.FechaRegistro = DateTime.Now;
            _lista.Add(presupuesto);
            return presupuesto.IdPresupuesto;
        }

        public bool EliminarLogico(int idPresupuesto)
        {
            var item = _lista.FirstOrDefault(p => p.IdPresupuesto == idPresupuesto);
            if (item == null) return false;

            item.Estado = false;
            return true;
        }
    }
}