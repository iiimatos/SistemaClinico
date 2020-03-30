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
            ViewBag.Ingresos_Registrado = new SelectList(db.Ingresos, "IdIngresos", "Fecha_Ingreso");
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

            ViewBag.Ingresos_Registrado = new SelectList(db.Ingresos, "IdIngresos", "Fecha_Ingreso", tAltaMedica.Ingresos_Registrado);
            return View(tAltaMedica);
        }

   

    }
}
