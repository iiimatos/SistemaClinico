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
    public class TCitasController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: TCitas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(t => t.Medicos).Include(t => t.Pacientes);
            return View(citas.ToList());
        }


        // GET: TCitas/Create
        public ActionResult Create()
        {
            ViewBag.Medicos_Registrado = new SelectList(db.Medicos, "IdMedicos", "Nombre");
            ViewBag.Pacientes_Registrado = new SelectList(db.Pacientes, "IdPacientes", "Nombre");
            return View();
        }

        // POST: TCitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCitas,Pacientes_Registrado,Fecha_Reserva,Hora_Reserva,Medicos_Registrado")] TCitas tCitas)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(tCitas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Medicos_Registrado = new SelectList(db.Medicos, "IdMedicos", "Nombre", tCitas.Medicos_Registrado);
            ViewBag.Pacientes_Registrado = new SelectList(db.Pacientes, "IdPacientes", "Nombre", tCitas.Pacientes_Registrado);
            return View(tCitas);
        }

    }
}
