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
using System.IO;
using PagedList;

namespace Motonet.Controllers
{
    public class AnnoncesController : Controller
    {
        private MotoContext db = new MotoContext();

        // GET: Annonces
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.PrixSortParm = sortOrder == "prix" ? "prix_desc" : "prix";
            ViewBag.CylindreeSortParm = sortOrder == "cylindree" ? "cylindree_desc" : "cylindree";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var annonces = from s in db.Annonces
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                annonces = annonces.Where(s => s.MotoProposee.Identification.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "date_desc":
                    annonces = annonces.OrderByDescending(s => s.Date);
                    break;
                case "cylindree":
                    annonces = annonces.OrderBy(s => s.MotoProposee.Cylindree);
                    break;
                case "cylindree_desc":
                    annonces = annonces.OrderByDescending(s => s.MotoProposee.Cylindree);
                    break;
                case "prix":
                    annonces = annonces.OrderBy(s => s.Prix);
                    break;
                case "prix_desc":
                    annonces = annonces.OrderByDescending(s => s.Prix);
                    break;
                default:
                    annonces = annonces.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(annonces.ToPagedList(pageNumber, pageSize));
        }

        // GET: Annonces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        // GET: Annonces/Create
        public ActionResult Create()
        {

            PopulateMotosDropDownLists();
            PopulateGenresDropDownList();
            PopulateMarquesDropDownList();
            PopulateDepartementsDropDownList();

            return View();
        }

        // POST: Annonces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titre,Description,MotoProposeeID,Annee,Kilometrage,Prix,MotosAccepteesID,MarquesAccepteesID,GenresAcceptesID,Nom,Mail,Telephone,DepartementID,MotDePasse,ConfirmationMotDePasse")] Annonce annonce, IEnumerable<HttpPostedFileBase> photos)
        {
            if (ModelState.IsValid)
            {
                foreach (var fichier in photos)
                {                    
                    if (fichier != null && fichier.ContentLength > 0)
                    {
                        var photo = new Photo
                        {
                            FileName = System.IO.Path.GetFileName(fichier.FileName),
                            ContentType = fichier.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(fichier.InputStream))
                        {
                            photo.Content = reader.ReadBytes(fichier.ContentLength);
                        }
                        annonce.Photos.Add(photo);
                    }
                }
                annonce.Photos = annonce.Photos.Take(3).ToList();

                annonce.Date = DateTime.Today;
                ProcessManyToManyRelationships(annonce);
                db.Annonces.Add(annonce);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = annonce.ID });
            }

            
            foreach (Moto moto in annonce.MotosAcceptees)
            {
                annonce.MotosAccepteesID.Add(moto.ID);
            }
            PopulateMotosDropDownLists(annonce.MotoProposeeID, annonce.MotosAccepteesID);
            
            
            foreach (Genre genre in annonce.GenresAcceptes)
            {
                annonce.GenresAcceptesID.Add(genre.ID);
            }
            PopulateGenresDropDownList(annonce.GenresAcceptesID);
              
                      
            foreach (Marque marque in annonce.MarquesAcceptees)
            {
                annonce.MarquesAccepteesID.Add(marque.ID);
            }
            PopulateMarquesDropDownList(annonce.MarquesAccepteesID);
            

            PopulateDepartementsDropDownList(annonce.DepartementID);

            return View(annonce);
        }

        // GET: Annonces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }

            
            foreach (Moto moto in annonce.MotosAcceptees)
            {
                annonce.MotosAccepteesID.Add(moto.ID);
            }
            PopulateMotosDropDownLists(annonce.MotoProposeeID, annonce.MotosAccepteesID);
            

            
            foreach (Genre genre in annonce.GenresAcceptes)
            {
                annonce.GenresAcceptesID.Add(genre.ID);
            }
            PopulateGenresDropDownList(annonce.GenresAcceptesID);
            

            
            foreach (Marque marque in annonce.MarquesAcceptees)
            {
                annonce.MarquesAccepteesID.Add(marque.ID);
            }
            PopulateMarquesDropDownList(annonce.MarquesAccepteesID);
            

            PopulateDepartementsDropDownList(annonce.DepartementID);

            return View(annonce);
        }

        // POST: Annonces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonceToUpdate = db.Annonces.Find(id);
            if (TryUpdateModel(annonceToUpdate, "",
               new string[] { "Titre", "Description", "MotoProposeeID", "Annee", "Kilometrage", "Prix", "MotosAccepteesID", "MarquesAccepteesID", "GenresAcceptesID", "Nom", "Mail", "Telephone", "DepartementID", "MotDePasse", "ConfirmationMotDePasse" }))
            {
                try
                {
                    annonceToUpdate.Date = DateTime.Today;
                    ProcessManyToManyRelationships(annonceToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            
            foreach (Moto moto in annonceToUpdate.MotosAcceptees)
            {
                annonceToUpdate.MotosAccepteesID.Add(moto.ID);
            }
            PopulateMotosDropDownLists(annonceToUpdate.MotoProposeeID, annonceToUpdate.MotosAccepteesID);
            

            
            foreach (Genre genre in annonceToUpdate.GenresAcceptes)
            {
                annonceToUpdate.GenresAcceptesID.Add(genre.ID);
            }
            PopulateGenresDropDownList(annonceToUpdate.GenresAcceptesID);
            

            
            foreach (Marque marque in annonceToUpdate.MarquesAcceptees)
            {
                annonceToUpdate.MarquesAccepteesID.Add(marque.ID);
            }
            PopulateMarquesDropDownList(annonceToUpdate.MarquesAccepteesID);
            

            PopulateDepartementsDropDownList(annonceToUpdate.DepartementID);

            return View(annonceToUpdate);
                        
        }

        // GET: Annonces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        // POST: Annonces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annonce annonce = db.Annonces.Find(id);
            db.Annonces.Remove(annonce);
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

        private void PopulateMotosDropDownLists(object selectedMoto = null, List<int> selectedMotos = null)
        {
            var motosQuery = from d in db.Motos
                              orderby d.Modele
                              select d;
            ViewBag.MotoProposeeID = new SelectList(motosQuery, "ID", "Identification", selectedMoto);
            ViewBag.MotosAccepteesID = new MultiSelectList(motosQuery, "ID", "Identification", selectedMotos);
        }

        private void PopulateGenresDropDownList(List<int> selectedGenres = null)
        {
            var genresQuery = from d in db.Genres
                             orderby d.Nom
                             select d;

            ViewBag.GenresAcceptesID = new MultiSelectList(genresQuery, "ID", "Nom", selectedGenres);
        }

        private void PopulateMarquesDropDownList(List<int> selectedMarques = null)
        {
            var marquesQuery = from d in db.Marques
                              orderby d.Nom
                              select d;

            ViewBag.MarquesAccepteesID = new MultiSelectList(marquesQuery, "ID", "Nom", selectedMarques);
        }

        private void PopulateDepartementsDropDownList(object selectedDepartement = null)
        {
            var departementsQuery = from d in db.Departements
                               orderby d.Nom
                               select d;

            ViewBag.DepartementID = new SelectList(departementsQuery, "ID", "Nom", selectedDepartement);
        }

        private void ProcessManyToManyRelationships(Annonce annonce)
        {
            
            annonce.MotosAcceptees.Clear();
            foreach (int motoID in annonce.MotosAccepteesID)
            {
                annonce.MotosAcceptees.Add(db.Motos.Find(motoID));
            }
                       
            annonce.MarquesAcceptees.Clear();
            foreach (int marqueID in annonce.MarquesAccepteesID)
            {
                annonce.MarquesAcceptees.Add(db.Marques.Find(marqueID));
            }
                        
            annonce.GenresAcceptes.Clear();
            foreach (int genreID in annonce.GenresAcceptesID)
            {
                annonce.GenresAcceptes.Add(db.Genres.Find(genreID));
            }
            
        }
    }
}
