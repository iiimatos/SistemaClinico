using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.Tablas;

namespace ProyectoFinal.Controllers.Procesos
{
    public class TIngresosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: TIngresos
        public ActionResult Index()
        {
            var ingresos = db.Ingresos.Include(t => t.Habitaciones).Include(t => t.Pacientes);
            return View(ingresos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string valor)
        {

            if (select == "fecha")
            {

                var consulta1 = db.Ingresos.Include(t => t.Habitaciones).Include(t => t.Pacientes).Where(a => a.Fecha_Ingreso == valor);
                return View(consulta1.ToList());

            }
            else if (select == "habitacion")
            {
                int s = (from g in db.Habitaciones where g.Numero == valor select g.IdHabitaciones).SingleOrDefault();

                var consulta1 = db.Ingresos.Include(t => t.Habitaciones).Include(t => t.Pacientes).Where(a => a.Habitaciones_Registrada.Equals(s));
                return View(consulta1.ToList());

            }
            var ingresos = db.Ingresos.Include(t => t.Habitaciones).Include(t => t.Pacientes);
            return View(ingresos.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Habitaciones_Registrada = new SelectList(db.Habitaciones, "IdHabitaciones", "Numero");
            ViewBag.Pacientes_Registrado = new SelectList(db.Pacientes, "IdPacientes", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIngresos,Pacientes_Registrado,Habitaciones_Registrada,Fecha_Ingreso")] TIngresos tIngresos)
        {
            if (ModelState.IsValid)
            {
                db.Ingresos.Add(tIngresos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Habitaciones_Registrada = new SelectList(db.Habitaciones, "IdHabitaciones", "Numero", tIngresos.Habitaciones_Registrada);
            ViewBag.Pacientes_Registrado = new SelectList(db.Pacientes, "IdPacientes", "Nombre", tIngresos.Pacientes_Registrado);
            return View(tIngresos);
        }

    }
}
