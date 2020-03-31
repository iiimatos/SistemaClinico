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
    public class MedicosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Medicos
        public ActionResult Index()
        {
            return View(db.Medicos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string valor)
        {
            if (select == "nombre")
            {
                var ingreso = from s in db.Medicos
                              select s;

                ingreso = ingreso.Where(s => s.Nombre.Contains(valor));


                return View(ingreso);

            }
            else if (select == "especialidad")
            {
                var ingreso = from s in db.Medicos
                              select s;

                ingreso = ingreso.Where(s => s.Especialidad.Contains(valor));


                return View(ingreso);
            }

            return View(db.Medicos.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMedicos,Nombre,Exequatur,Especialidad")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medicos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicos);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMedicos,Nombre,Exequatur,Especialidad")] Medicos medicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicos);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicos medicos = db.Medicos.Find(id);
            db.Medicos.Remove(medicos);
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
