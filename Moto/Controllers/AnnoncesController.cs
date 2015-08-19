using Motonet.DAL;
using Motonet.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Motonet.Controllers
{
    public class AnnoncesController : Controller
    {
        private MotoContext db = new MotoContext();

        // Liste toutes les annonces autorisées et validées
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

            annonces = annonces.Where(s => s.Autorisee == true && s.Validee == true);
                        
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

        // Liste toutes les annonces non validées
        public ActionResult AnnoncesAValider(string sortOrder, int? id)
        {
            var annonces = from s in db.Annonces
                           select s;

            annonces = annonces.Where(s => s.Autorisee == false && s.Validee == false);

            if (id != null)
            {
                if (id != -1)
                {
                    Annonce annonceAValider = db.Annonces.Find(id);
                    annonceAValider.ConfirmerMotDePasse = annonceAValider.MotDePasse;
                    annonceAValider.Validee = true;
                }
                else
                {
                    foreach (Annonce annonceAValider in annonces)
                    {
                        annonceAValider.ConfirmerMotDePasse = annonceAValider.MotDePasse;
                        annonceAValider.Validee = true;
                    }
                }
                db.SaveChanges();
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.PrixSortParm = sortOrder == "prix" ? "prix_desc" : "prix";
            ViewBag.CylindreeSortParm = sortOrder == "cylindree" ? "cylindree_desc" : "cylindree";
            ViewBag.KilometrageSortParm = sortOrder == "kilometrage" ? "kilometrage_desc" : "kilometrage";



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

            return View(annonces);
        }

        // Liste toutes les annonces non autorisées et validées
        public ActionResult AnnoncesAAutoriser(string sortOrder, int? id)
        {
            var annonces = from s in db.Annonces
                           select s;

            annonces = annonces.Where(s => s.Autorisee == false && s.Validee == true);

            if (id != null)
            {
                if (id != -1)
                {
                    Annonce annonceAAutoriser = db.Annonces.Find(id);
                    annonceAAutoriser.ConfirmerMotDePasse = annonceAAutoriser.MotDePasse;
                    annonceAAutoriser.Autorisee = true;
                }
                else
                {
                    foreach (Annonce annonceAAutoriser in annonces)
                    {
                        annonceAAutoriser.ConfirmerMotDePasse = annonceAAutoriser.MotDePasse;
                        annonceAAutoriser.Autorisee = true;
                    }
                }
                db.SaveChanges();
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.PrixSortParm = sortOrder == "prix" ? "prix_desc" : "prix";
            ViewBag.CylindreeSortParm = sortOrder == "cylindree" ? "cylindree_desc" : "cylindree";
            ViewBag.KilometrageSortParm = sortOrder == "kilometrage" ? "kilometrage_desc" : "kilometrage";

               

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

            return View(annonces);
        }

        // Affiche les détails d'une annonce en particulier
        [AllowAnonymous]
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

        // Affiche le formulaire de création d'une annonce (premier affichage)
        [AllowAnonymous]
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

        // Affiche le formulaire de création d'une annonce (affichages suivants)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Titre,Description,MotoProposeeID,Annee,Kilometrage,Prix,MotosAccepteesID,MarquesAccepteesID,GenresAcceptesID,Nom,Mail,Telephone,DepartementID,MotDepasse,ConfirmerMotDePasse")] Annonce annonce, IEnumerable<HttpPostedFileBase> photos)
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

                // On hash le mot de passe
                annonce.MotDePasse = Annonce.HashPassword(annonce.MotDePasse);
                annonce.ConfirmerMotDePasse = annonce.MotDePasse;

                // L'annonce est faite à la date du jour
                annonce.Date = DateTime.Today;

                // On ajoute les modèles, marques et genres acceptés
                ProcessManyToManyRelationships(annonce);

                db.Annonces.Add(annonce);
                db.SaveChanges();
                
                // On envoie un mail pour valider l'adresse
                var email = new MailValiderAdresse
                {
                    Destinataire = annonce.Mail,
                    Nom = annonce.Nom,
                    Lien = Url.Action("ValidateMail", "Annonces", new { annonceId = annonce.ID.ToString(), code = annonce.CodeValidation }, Request.Url.Scheme)
                };
                
                email.Send();

                return RedirectToAction("Index");
                
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

        // Affiche la demande de mot de passe avant de pouvoir éditer l'annonce
        [AllowAnonymous]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            // On récupère l'annonce
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }

            ViewBag.AnnonceID = annonce.ID;
            ViewBag.actionName = "EditPassword";
            return View();
        }

        // Affiche le formulaire pour éditer l'annonce (premier affichage)
        [HttpPost, ActionName("EditPassword")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult EditPostPassword(int? annonceID, string password)
        {

            if (annonceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonce = db.Annonces.Find(annonceID);


            // On vérifie que le code saisi est le bon (une fois hashé)
            if (!Annonce.VerifyHashedPassword(annonce.MotDePasse, password))
            {
                ViewBag.Message = "Mot de passe incorrect";
                ViewBag.AnnonceID = annonce.ID;
                ViewBag.actionName = "EditPassword";
                return View("Edit");
            }


            // A ce niveau, le mot de passe a été renseigné et est correct
            // On affiche la page d'édition de l'annonce
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

            ViewBag.password = password;

            return View("Edit", annonce);
                        
        }

        // Affiche le formulaire pour éditer l'annonce (affichages suivants)
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult EditPost(int? id, string password, IEnumerable<HttpPostedFileBase> photos)
        {

            int tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]);
            int nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonceToUpdate = db.Annonces.Find(id);


            // On vérifie que le code saisi est le bon (une fois hashé)
            if (!Annonce.VerifyHashedPassword(annonceToUpdate.MotDePasse, password))
            {
                ViewBag.Message = "Mot de passe incorrect";
                return View();
            }

            annonceToUpdate.ConfirmerMotDePasse = annonceToUpdate.MotDePasse;

            if (TryUpdateModel(annonceToUpdate, "",
               new string[] { "Titre", "Description", "MotoProposeeID", "Annee", "Kilometrage", "Prix", "MotosAccepteesID", "MarquesAccepteesID", "GenresAcceptesID", "Nom", "Mail", "Telephone", "DepartementID" }))
            {
                try
                {
                    if (photos != null && photos.First() != null)
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

        // Affiche la demande de mot de passe avant de pouvoir supprimer l'annonce
        [AllowAnonymous]
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

            ViewBag.AnnonceID = annonce.ID;
            ViewBag.actionName = "DeletePassword";
            ViewBag.supprimee = false;
            return View();
        }

        // Affiche le formulaire pour supprimer l'annonce (premier affichage)
        [HttpPost, ActionName("DeletePassword")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult DeletePostPassword(int? annonceID, string password)
        {

            if (annonceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonce = db.Annonces.Find(annonceID);


            // On vérifie que le code saisi est le bon (une fois hashé)
            if (!Annonce.VerifyHashedPassword(annonce.MotDePasse, password))
            {
                ViewBag.Message = "Mot de passe incorrect";
                ViewBag.AnnonceID = annonce.ID;
                ViewBag.actionName = "DeletePassword";
                ViewBag.supprimee = false;
                return View("Delete");
            }

            // A ce niveau, le mot de passe a été renseigné et est correct
            // On affiche la page de suppression de l'annonce

            db.Annonces.Remove(annonce);
            db.SaveChanges();


            ViewBag.supprimee = true;
            return View("Delete");

        }

        // Affiche la page de validation de l'adresse (et donc de l'annonce) si le code fourni est le bon
        [AllowAnonymous]
        public ActionResult ValidateMail(int annonceId, string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Annonce annonce = db.Annonces.Find(annonceId);

            if (annonce == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!code.Equals(annonce.CodeValidation))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            annonce.ConfirmerMotDePasse = annonce.MotDePasse;
            annonce.Validee = true;

            db.SaveChanges();
            
            return View();
        }

        // Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Peuple les listes déroulantes des modèles moto
        private void PopulateMotosDropDownLists(object selectedMoto = null, List<int> selectedMotos = null)
        {
            var motosQuery = from d in db.Motos
                              orderby d.Modele
                              select d;
            ViewBag.MotoProposeeID = new SelectList(motosQuery, "ID", "Identification", selectedMoto);
            ViewBag.MotosAccepteesID = new MultiSelectList(motosQuery, "ID", "Identification", selectedMotos);
        }

        // Peuple la liste déroulante des genres moto
        private void PopulateGenresDropDownList(List<int> selectedGenres = null)
        {
            var genresQuery = from d in db.Genres
                             orderby d.Nom
                             select d;

            ViewBag.GenresAcceptesID = new MultiSelectList(genresQuery, "ID", "Nom", selectedGenres);
        }

        // Peuple la liste déroulante des marques moto
        private void PopulateMarquesDropDownList(List<int> selectedMarques = null)
        {
            var marquesQuery = from d in db.Marques
                              orderby d.Nom
                              select d;

            ViewBag.MarquesAccepteesID = new MultiSelectList(marquesQuery, "ID", "Nom", selectedMarques);
        }

        // Peuple la liste déroulante des départements
        private void PopulateDepartementsDropDownList(object selectedDepartement = null)
        {
            var departementsQuery = from d in db.Departements
                               orderby d.Nom
                               select d;

            ViewBag.DepartementID = new SelectList(departementsQuery, "ID", "Nom", selectedDepartement);
        }

        // Rempli les listes des paramètres virtuels concernant les relations many-many
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

        // Redimensionne l'image en gardant son ratio et en étant inférieur au maximum de largeur et hauteur
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

        // Image vers Byte array
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
              
        // Récupérer le type Mime de l'image
        private string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }

    }
}
