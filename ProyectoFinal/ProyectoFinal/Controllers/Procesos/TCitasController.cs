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

        public ActionResult Index()
        {
            var citas = db.Citas.Include(t => t.Medicos).Include(t => t.Pacientes);
            return View(citas.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string valor)
        {

            if (select == "fecha")
            {

                var consulta1 = db.Citas.Include(t => t.Medicos).Include(t => t.Pacientes).Where(a => a.Fecha_Reserva == valor);
                return View(consulta1.ToList());

            }
            else if (select == "medico")
            {
                int s = (from g in db.Medicos where g.Nombre == valor select g.IdMedicos).SingleOrDefault();

                var consulta1 = db.Citas.Include(t => t.Medicos).Include(t => t.Pacientes).Where(a => a.Medicos_Registrado.Equals(s));
                return View(consulta1.ToList());

            }else if (select=="paciente")
            {
                int s = (from g in db.Pacientes where g.Nombre == valor select g.IdPacientes).SingleOrDefault();

                var consulta1 = db.Citas.Include(t => t.Medicos).Include(t => t.Pacientes).Where(a => a.Pacientes_Registrado.Equals(s));
                return View(consulta1.ToList());
            }
            var ingresos = db.Ingresos.Include(t => t.Habitaciones).Include(t => t.Pacientes);
            return View(ingresos.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Medicos_Registrado = new SelectList(db.Medicos, "IdMedicos", "Nombre");
            ViewBag.Pacientes_Registrado = new SelectList(db.Pacientes, "IdPacientes", "Nombre");
            return View();
        }
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
