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

namespace Motonet.Controllers
{
    public class MarquesController : Controller
    {
        private MotoContext db = new MotoContext();

        // GET: Marques
        public ActionResult Index()
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            return View(db.Marques.ToList());
        }

        // GET: Marques/Details/5
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
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // GET: Marques/Create
        public ActionResult Create()
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            return View();
        }

        // POST: Marques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nom")] Marque marque)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Marques.Add(marque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marque);
        }

        // GET: Marques/Edit/5
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
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: Marques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nom")] Marque marque)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(marque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marque);
        }

        // GET: Marques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: Marques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            Marque marque = db.Marques.Find(id);
            db.Marques.Remove(marque);
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

        public ActionResult ListePartielleMarques(string q)
        {
            MotoContext mc = new MotoContext();

            if (String.IsNullOrEmpty(q))
            {
                return null;
            }

            var suggestions = from s in mc.Marques
                              select new
                              {
                                  id = s.ID,
                                  value = s.Nom
                              };
            var motoList = suggestions.ToList().Where(n => n.value.ToLower().Contains(q.ToLower())).Take(20);

            return Json(motoList.Select(m => new
            {
                id = m.id,
                text = m.value
            }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult MarqueEnParticulier(string q)
        {
            MotoContext mc = new MotoContext();

            if (String.IsNullOrEmpty(q))
            {
                return null;
            }

            var suggestions = from s in mc.Marques
                              select new
                              {
                                  id = s.ID,
                                  value = s.Nom
                              };

            List<int> liste = q.Split('/').Select(Int32.Parse).ToList();

            var marquesList = suggestions.ToList().Where(n => liste.Contains(n.id));

            return Json(marquesList.Select(m => new
            {
                id = m.id,
                text = m.value
            }), JsonRequestBehavior.AllowGet);

        }
    }
}
