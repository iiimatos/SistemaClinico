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

namespace ProyectoFinal.Controllers.Mantenimiento
{
    public class PacientesController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Pacientes
        public ActionResult Index()
        {
            return View(db.Pacientes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string valor)
        {
            if (select == "nombre")
            {
                var ingreso = from s in db.Pacientes
                              select s;

                ingreso = ingreso.Where(s => s.Nombre.Contains(valor));


                return View(ingreso);

            }
            else if (select == "asegurado")
            {
                if (valor == "si" || valor == "Si" || valor == "SI" || valor == "sI")
                {
                    var ingreso = from a in db.Pacientes

                                  where a.Asegurado.Equals(true)

                                  select a;


                    return View(ingreso);
                }
                else if (valor == "no" || valor == "No" || valor == "NO" || valor == "nO")
                {
                    var ingreso = from a in db.Pacientes

                                  where a.Asegurado.Equals(false)

                                  select a;


                    return View(ingreso);

                }

                return View(db.Pacientes.ToList());

            }
            else if (select == "cedula")
            {
                var ingreso = from s in db.Pacientes
                              select s;

                ingreso = ingreso.Where(s => s.Cedula.Contains(valor));


                return View(ingreso);
            }
            else
            {

                return View(db.Pacientes.ToList());

            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPacientes,Cedula,Nombre,Asegurado")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                db.Pacientes.Add(pacientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPacientes,Cedula,Nombre,Asegurado")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacientes pacientes = db.Pacientes.Find(id);
            db.Pacientes.Remove(pacientes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
