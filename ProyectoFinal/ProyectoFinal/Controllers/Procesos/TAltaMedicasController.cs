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

        [HttpPost]
        public ActionResult Index(string select, string valor)
        {

            if (select == "paciente")
            {

                var consulta1 = db.AltaMedicas.Include(t => t.Ingresos).Where(t => t.Paciente == valor);

                ViewBag.total = consulta1.Sum(a => a.montoPagar);
                ViewBag.conteo = consulta1.Count();
                ViewBag.min = consulta1.Min(a => a.montoPagar);
                ViewBag.max = consulta1.Max(a => a.montoPagar);
                ViewBag.prom = consulta1.Average(a => a.montoPagar);

                return View(consulta1.ToList());

            }
            else if (select == "fecha")
            {
                int s = (from g in db.Medicos where g.Nombre == valor select g.IdMedicos).SingleOrDefault();

                var consulta1 = db.AltaMedicas.Include(t => t.Ingresos).Where(t => t.fechaSalida == valor);

                ViewBag.total = consulta1.Sum(a => a.montoPagar);
                ViewBag.conteo = consulta1.Count();
                ViewBag.min = consulta1.Min(a => a.montoPagar);
                ViewBag.max = consulta1.Max(a => a.montoPagar);
                ViewBag.prom = consulta1.Average(a => a.montoPagar);

                return View(consulta1.ToList());

            }
            var ingresos = db.Ingresos.Include(t => t.Habitaciones).Include(t => t.Pacientes);
            return View(ingresos.ToList());
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
