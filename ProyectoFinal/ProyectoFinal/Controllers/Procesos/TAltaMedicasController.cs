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
    public class TAltaMedicasController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        public ActionResult Index()
        {
            var altaMedicas = db.AltaMedicas.Include(t => t.Ingresos);
            return View(altaMedicas.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Ingresos_Registrado = new SelectList(db.Ingresos, "IdIngresos", "IdIngresos");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAltaMedica,fechaIngreso,Paciente,Habitacion,fechaSalida,montoPagar,Ingresos_Registrado")] TAltaMedica tAltaMedica)
        {
            if (ModelState.IsValid)
            {
                db.AltaMedicas.Add(tAltaMedica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ingresos_Registrado = new SelectList(db.Ingresos, "IdIngresos", "IdIngresos", tAltaMedica.Ingresos_Registrado);
            return View(tAltaMedica);
        }

        public JsonResult Nombre(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join p in db.Pacientes
                             on i.Pacientes_Registrado equals p.IdPacientes
                             where i.IdIngresos == clavePaciente
                             select p.Nombre).ToList();
            return Json(duplicado);
        }

        public JsonResult Monto(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.Habitaciones_Registrada equals h.IdHabitaciones
                             where i.IdIngresos == clavePaciente
                             select h.Precio).ToList();
            return Json(duplicado);
        }

        public JsonResult FechaIngreso(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             where i.IdIngresos == clavePaciente
                             select i.Fecha_Ingreso).ToList();
            return Json(duplicado);
        }

        public JsonResult NumeroHabitacion(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.Habitaciones_Registrada equals h.IdHabitaciones
                             where i.IdIngresos == clavePaciente
                             select h.Numero).ToList();
            return Json(duplicado);
        }




    }
}
