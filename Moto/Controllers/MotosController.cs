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
using PagedList;

namespace Motonet.Controllers
{
    public class MotosController : Controller
    {
        private MotoContext db = new MotoContext();

        // GET: Motos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ModeleSortParm = String.IsNullOrEmpty(sortOrder) ? "modele_desc" : "";
            ViewBag.MarqueSortParm = sortOrder == "marque" ? "marque_desc" : "marque";
            ViewBag.CylindreeSortParm = sortOrder == "cylindree" ? "cylindree_desc" : "cylindree";
            ViewBag.GenreSortParm = sortOrder == "genre" ? "genre_desc" : "genre";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            
            
            var motos = from s in db.Motos
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                motos = motos.Where(s => s.Modele.Contains(searchString)
                                       || s.Marque.Nom.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "modele_desc":
                    motos = motos.OrderByDescending(s => s.Marque.Nom).ThenByDescending(s => s.Modele);
                    break;
                case "marque":
                    motos = motos.OrderBy(s => s.Marque.Nom);
                    break;
                case "marque_desc":
                    motos = motos.OrderByDescending(s => s.Marque.Nom);
                    break;
                case "cylindree":
                    motos = motos.OrderBy(s => s.Cylindree);
                    break;
                case "cylindree_desc":
                    motos = motos.OrderByDescending(s => s.Cylindree);
                    break;
                case "genre":
                    motos = motos.OrderBy(s => s.Genre.Nom);
                    break;
                case "genre_desc":
                    motos = motos.OrderByDescending(s => s.Genre.Nom);
                    break;
                default:
                    motos = motos.OrderBy(s => s.Marque.Nom).ThenBy(s => s.Modele);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(motos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Motos/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

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
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

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
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

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
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

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
        public ActionResult EditPost(int? id)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var motoToUpdate = db.Motos.Find(id);
            if (TryUpdateModel(motoToUpdate, "",
               new string[] { "Modele", "Cylindree", "MarqueID", "GenreID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateGenresDropDownList(motoToUpdate.GenreID);
            PopulateMarquesDropDownList(motoToUpdate.MarqueID);
            return View(motoToUpdate);
        }

        // GET: Motos/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
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
        public ActionResult Delete(int id)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            try
            {
                Moto moto = db.Motos.Find(id);
                db.Motos.Remove(moto);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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

        public ActionResult ListePartielleMotos(string q)
        {
            MotoContext mc = new MotoContext();

            if (String.IsNullOrEmpty(q))
            {
                return null;
            }

            var suggestions = from s in mc.Motos
                              select new
                              {
                                  id = s.ID,
                                  value = s.Marque.Nom + " " + s.Modele + " (" + s.Cylindree + ")"
                              };
            var motoList = suggestions.ToList().Where(n => n.value.ToLower().Contains(q.ToLower())).Take(40);

            return Json(motoList.Select(m => new
            {
                id = m.id,
                text = m.value
            }), JsonRequestBehavior.AllowGet);
    
        }

        public ActionResult MotoEnParticulier(string q)
        {
            MotoContext mc = new MotoContext();

            if (String.IsNullOrEmpty(q))
            {
                return null;
            }

            var suggestions = from s in mc.Motos
                              select new
                              {
                                  id = s.ID,
                                  value = s.Marque.Nom + " " + s.Modele + " (" + s.Cylindree + ")"
                              };

            List<int> liste = q.Split('/').Select(Int32.Parse).ToList();

            var motoList = suggestions.ToList().Where(n => liste.Contains(n.id));

            return Json(motoList.Select(m => new
            {
                id = m.id,
                text = m.value
            }), JsonRequestBehavior.AllowGet);

        }

    }
}
