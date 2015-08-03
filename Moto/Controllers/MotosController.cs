using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Motonet.DAL;
using Motonet.Models;
using System.Data.Entity.Infrastructure;

namespace Motonet.Controllers
{
    public class MotosController : Controller
    {
        private MotoContext db = new MotoContext();

        // GET: Motos
        public ActionResult Index()
        {
            return View(db.Motos.ToList());
        }

        // GET: Motos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moto moto = db.Motos.Find(id);
            if (moto == null)
            {
                return HttpNotFound();
            }
            return View(moto);
        }

        // GET: Motos/Create
        public ActionResult Create()
        {
            PopulateGenresDropDownList();
            PopulateMarquesDropDownList();
            return View();
        }

        // POST: Motos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Modele,Cylindree,GenreID,MarqueID")] Moto moto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Motos.Add(moto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateGenresDropDownList(moto.GenreID);
            PopulateMarquesDropDownList(moto.MarqueID);
            return View(moto);
        }

        // GET: Motos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moto moto = db.Motos.Find(id);
            if (moto == null)
            {
                return HttpNotFound();
            }
            PopulateGenresDropDownList(moto.GenreID);
            PopulateMarquesDropDownList(moto.MarqueID);
            return View(moto);
        }

        // POST: Motos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Modele,Cylindree,GenreID,MarqueID")] Moto moto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateGenresDropDownList(moto.GenreID);
            PopulateMarquesDropDownList(moto.MarqueID);
            return View(moto);
        }

        // GET: Motos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moto moto = db.Motos.Find(id);
            if (moto == null)
            {
                return HttpNotFound();
            }
            return View(moto);
        }

        // POST: Motos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Moto moto = db.Motos.Find(id);
            db.Motos.Remove(moto);
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

        private void PopulateGenresDropDownList(object selectedGenre = null)
        {
            var genresQuery = from d in db.Genres
                                   orderby d.Nom
                                   select d;
            ViewBag.GenreID = new SelectList(genresQuery, "ID", "Nom", selectedGenre);
        }

        private void PopulateMarquesDropDownList(object selectedMarque = null)
        {
            var marquesQuery = from d in db.Marques
                              orderby d.Nom
                              select d;
            ViewBag.MarqueID = new SelectList(marquesQuery, "ID", "Nom", selectedMarque);
        }
    }
}
