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
using System.Web.Helpers;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Configuration;

namespace Motonet.Controllers
{
    public class AnnoncesController : Controller
    {
        private MotoContext db = new MotoContext();

        // GET: Annonces
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.PrixSortParm = sortOrder == "prix" ? "prix_desc" : "prix";
            ViewBag.CylindreeSortParm = sortOrder == "cylindree" ? "cylindree_desc" : "cylindree";
            ViewBag.KilometrageSortParm = sortOrder == "kilometrage" ? "kilometrage_desc" : "kilometrage";

            
            var annonces = from s in db.Annonces
                        select s;
                        
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
                case "kilometrage":
                    annonces = annonces.OrderBy(s => s.Kilometrage);
                    break;
                case "kilometrage_desc":
                    annonces = annonces.OrderByDescending(s => s.Kilometrage);
                    break;
                default:
                    annonces = annonces.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
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

            ViewBag.tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]) / 1024;
            ViewBag.nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);
            ViewBag.nombreMaxCaracteresDescription = int.Parse(ConfigurationManager.AppSettings["nombreMaxCaracteresDescription"]);

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
        public ActionResult Create([Bind(Include = "Titre,Description,MotoProposeeID,Annee,Kilometrage,Prix,MotosAccepteesID,MarquesAccepteesID,GenresAcceptesID,Nom,Mail,Telephone,DepartementID")] Annonce annonce, IEnumerable<HttpPostedFileBase> photos)
        {

            int tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]);
            int nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);

            if (ModelState.IsValid)
            {
                
                // On parcourt les fichiers sélectionnés (dans la limite de "nombreMaxdePhotos") afin de les redimensionner et les stocker
                for (int i = 0; i < ((nombreMaxdePhotos < photos.Count()) ? nombreMaxdePhotos : photos.Count()); i++)
                {
                    HttpPostedFileBase fichier = photos.ElementAt(i);

                    if (fichier != null && fichier.ContentLength > 0 && fichier.ContentLength < tailleMaxiUploadEnOctet)
                    {
                        Image imageOriginale = Image.FromStream(fichier.InputStream, true, true);

                        // Ajout de la version Miniature de cette image
                        int largeurMaxMiniature = int.Parse(ConfigurationManager.AppSettings["largeurMaxMiniature"]);
                        int hauteurMaxMiniature = int.Parse(ConfigurationManager.AppSettings["hauteurMaxMiniature"]);

                        Image imageRedimensionneeMiniature = ScaleImage(imageOriginale, largeurMaxMiniature, hauteurMaxMiniature);

                        var photoMiniature = new Photo
                        {
                            Taille = Photo.TypeTaille.Miniature,
                            ContentType = fichier.ContentType,
                            Content = imageToByteArray(imageRedimensionneeMiniature, fichier.ContentType)
                        };
                                          
                        annonce.Photos.Add(photoMiniature);
                        
                        // Ajout de la version Vignette de cette image
                        int largeurMaxVignette = int.Parse(ConfigurationManager.AppSettings["largeurMaxVignette"]);
                        int hauteurMaxVignette = int.Parse(ConfigurationManager.AppSettings["hauteurMaxVignette"]);

                        Image imageRedimensionneeVignette = ScaleImage(imageOriginale, largeurMaxVignette, hauteurMaxVignette);

                        var photoVignette = new Photo
                        {
                            Taille = Photo.TypeTaille.Vignette,
                            ContentType = fichier.ContentType,
                            Content = imageToByteArray(imageRedimensionneeVignette, fichier.ContentType)
                        };

                        annonce.Photos.Add(photoVignette);
                    }
                }

                // L'annonce est faite à la date du jour
                annonce.Date = DateTime.Today;

                // On ajoute les modèles, marques et genres acceptés
                ProcessManyToManyRelationships(annonce);

                db.Annonces.Add(annonce);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = annonce.ID });
            }

            ViewBag.tailleMaxiUploadEnOctet = tailleMaxiUploadEnOctet / 1024;
            ViewBag.nombreMaxdePhotos = nombreMaxdePhotos;
            ViewBag.nombreMaxCaracteresDescription = int.Parse(ConfigurationManager.AppSettings["nombreMaxCaracteresDescription"]);
            
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

            ViewBag.tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]) / 1024;
            ViewBag.nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);
            ViewBag.nombreMaxCaracteresDescription = int.Parse(ConfigurationManager.AppSettings["nombreMaxCaracteresDescription"]);
            

            
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
        public ActionResult EditPost(int? id, IEnumerable<HttpPostedFileBase> photos)
        {

            int tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]);
            int nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonceToUpdate = db.Annonces.Find(id);
            if (TryUpdateModel(annonceToUpdate, "",
               new string[] { "Titre", "Description", "MotoProposeeID", "Annee", "Kilometrage", "Prix", "MotosAccepteesID", "MarquesAccepteesID", "GenresAcceptesID", "Nom", "Mail", "Telephone", "DepartementID" }))
            {
                try
                {
                    if (photos.First() != null)
                    {
                        // On vide la liste pour mettre uniquement les nouvelles photos.
                        db.Photos.RemoveRange(annonceToUpdate.Photos);
                        db.SaveChanges();

                        // On parcourt les fichiers sélectionnés (dans la limite de "nombreMaxdePhotos") afin de les redimensionner et les stocker
                        for (int i = 0; i < ((nombreMaxdePhotos < photos.Count()) ? nombreMaxdePhotos : photos.Count()); i++)
                        {
                            HttpPostedFileBase fichier = photos.ElementAt(i);

                            if (fichier != null && fichier.ContentLength > 0 && fichier.ContentLength < tailleMaxiUploadEnOctet)
                            {
                                Image imageOriginale = Image.FromStream(fichier.InputStream, true, true);

                                // Ajout de la version Miniature de cette image
                                int largeurMaxMiniature = int.Parse(ConfigurationManager.AppSettings["largeurMaxMiniature"]);
                                int hauteurMaxMiniature = int.Parse(ConfigurationManager.AppSettings["hauteurMaxMiniature"]);

                                Image imageRedimensionneeMiniature = ScaleImage(imageOriginale, largeurMaxMiniature, hauteurMaxMiniature);

                                var photoMiniature = new Photo
                                {
                                    Taille = Photo.TypeTaille.Miniature,
                                    ContentType = fichier.ContentType,
                                    Content = imageToByteArray(imageRedimensionneeMiniature, fichier.ContentType)
                                };

                                annonceToUpdate.Photos.Add(photoMiniature);

                                // Ajout de la version Vignette de cette image
                                int largeurMaxVignette = int.Parse(ConfigurationManager.AppSettings["largeurMaxVignette"]);
                                int hauteurMaxVignette = int.Parse(ConfigurationManager.AppSettings["hauteurMaxVignette"]);

                                Image imageRedimensionneeVignette = ScaleImage(imageOriginale, largeurMaxVignette, hauteurMaxVignette);

                                var photoVignette = new Photo
                                {
                                    Taille = Photo.TypeTaille.Vignette,
                                    ContentType = fichier.ContentType,
                                    Content = imageToByteArray(imageRedimensionneeVignette, fichier.ContentType)
                                };

                                annonceToUpdate.Photos.Add(photoVignette);
                            }
                        }
                    }


                    annonceToUpdate.Date = DateTime.Today;
                    ProcessManyToManyRelationships(annonceToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = annonceToUpdate.ID });
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

            ViewBag.tailleMaxiUploadEnOctet = tailleMaxiUploadEnOctet / 1024;
            ViewBag.nombreMaxdePhotos = nombreMaxdePhotos;
            ViewBag.nombreMaxCaracteresDescription = int.Parse(ConfigurationManager.AppSettings["nombreMaxCaracteresDescription"]);
            

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

        private Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }        

        private byte[] imageToByteArray(System.Drawing.Image imageIn, String ContentType)
        {
            MemoryStream ms = new MemoryStream();

            if (ContentType.Equals(GetMimeType(System.Drawing.Imaging.ImageFormat.Bmp)))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            else if (ContentType.Equals(GetMimeType(System.Drawing.Imaging.ImageFormat.Gif)))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            }
            else if (ContentType.Equals(GetMimeType(System.Drawing.Imaging.ImageFormat.Jpeg)))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (ContentType.Equals(GetMimeType(System.Drawing.Imaging.ImageFormat.Png)))
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            }                    

            return ms.ToArray();
        }
              
        private string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }
    }
}
