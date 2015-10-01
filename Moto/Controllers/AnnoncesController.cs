using Motonet.DAL;
using Motonet.Models;
using PagedList;
using Postal;
using System;
using System.Collections;
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
        public ActionResult Index(string sortOrder, int? page, string currentFilterTitre,
            string currentFilterMoto, string currentFilterAnneeMin, string currentFilterAnneeMax,
            string currentFilterKilometrageMin, string currentFilterKilometrageMax, string currentFilterPrixMin,
            string currentFilterPrixMax, string currentFilterCylindreeMin, string currentFilterCylindreeMax, List<int> currentFilterRegionID, List<int> currentFilterDepartementID, string currentFilterMaMoto, string currentFilterIdMaMoto,
            string FiltreTitre, string FiltreMoto, string FiltreAnneeMin, string FiltreAnneeMax,
            string FiltreKilometrageMin, string FiltreKilometrageMax, string FiltrePrixMin,
            string FiltrePrixMax, string FiltreCylindreeMin, string FiltreCylindreeMax, List<int> RegionsID, List<int> DepartementsID, string FiltreMaMoto, string FiltreIdMaMoto)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "date_desc" ? "date" : "date_desc";
            ViewBag.PrixSortParm = sortOrder == "prix" ? "prix_desc" : "prix";
            ViewBag.AnneeSortParm = sortOrder == "annee" ? "annee_desc" : "annee";
            ViewBag.KilometrageSortParm = sortOrder == "kilometrage" ? "kilometrage_desc" : "kilometrage";


            if (FiltreTitre != null || FiltreMoto != null || FiltreAnneeMin != null || FiltreAnneeMax != null ||
                FiltreKilometrageMin != null || FiltreKilometrageMax != null ||
                FiltrePrixMin != null || FiltrePrixMax != null ||
                FiltreCylindreeMin != null || FiltreCylindreeMax != null || RegionsID != null || DepartementsID != null || FiltreIdMaMoto != null)
            {
                page = 1;
            }
            else
            {
                FiltreTitre = currentFilterTitre;
                FiltreMoto = currentFilterMoto;
                FiltreAnneeMin = currentFilterAnneeMin;
                FiltreAnneeMax = currentFilterAnneeMax;
                FiltreKilometrageMin = currentFilterKilometrageMin;
                FiltreKilometrageMax = currentFilterKilometrageMax;
                FiltrePrixMin = currentFilterPrixMin;
                FiltrePrixMax = currentFilterPrixMax;
                FiltreCylindreeMin = currentFilterCylindreeMin;
                FiltreCylindreeMax = currentFilterCylindreeMax;
                RegionsID = currentFilterRegionID;
                DepartementsID = currentFilterDepartementID;
                FiltreMaMoto = currentFilterMaMoto;
                FiltreIdMaMoto = currentFilterIdMaMoto;
            }

            ViewBag.CurrentFilterTitre = FiltreTitre;
            ViewBag.CurrentFilterMoto = FiltreMoto;
            ViewBag.CurrentFilterAnneeMin = FiltreAnneeMin;
            ViewBag.CurrentFilterAnneeMax = FiltreAnneeMax;
            ViewBag.CurrentFilterKilometrageMin = FiltreKilometrageMin;
            ViewBag.CurrentFilterKilometrageMax = FiltreKilometrageMax;
            ViewBag.CurrentFilterPrixMin = FiltrePrixMin;
            ViewBag.CurrentFilterPrixMax = FiltrePrixMax;
            ViewBag.CurrentFilterCylindreeMin = FiltreCylindreeMin;
            ViewBag.CurrentFilterCylindreeMax = FiltreCylindreeMax;
            ViewBag.CurrentFilterMaMoto = FiltreMaMoto;
            ViewBag.CurrentFilterIdMaMoto = FiltreIdMaMoto;
             

            var annonces = db.Annonces.ToList().Where(s => s.Autorisee == true && s.Validee == true);


            int intFiltreAnneeMin;
            int intFiltreAnneeMax;
            int intFiltreKilometrageMin;
            int intFiltreKilometrageMax;
            int intFiltrePrixMin;
            int intFiltrePrixMax;
            int intFiltreCylindreeMin;
            int intFiltreCylindreeMax;
            int intFiltreIdMaMoto;


            if (!String.IsNullOrEmpty(FiltreTitre))
            {
                annonces = annonces.Where(s => s.Titre.Contains(FiltreTitre));
            }

            if (!String.IsNullOrEmpty(FiltreMoto))
            {
                annonces = annonces.Where(s => s.MotoProposee.Modele.Contains(FiltreMoto) || s.MotoProposee.Marque.Nom.Contains(FiltreMoto));
            }

            if (!String.IsNullOrEmpty(FiltreAnneeMin) && int.TryParse(FiltreAnneeMin, out intFiltreAnneeMin))
            {
                annonces = annonces.Where(s => s.Annee >= intFiltreAnneeMin);
            }

            if (!String.IsNullOrEmpty(FiltreAnneeMax) && int.TryParse(FiltreAnneeMax, out intFiltreAnneeMax))
            {
                annonces = annonces.Where(s => s.Annee <= intFiltreAnneeMax);
            }

            if (!String.IsNullOrEmpty(FiltreKilometrageMin) && int.TryParse(FiltreKilometrageMin, out intFiltreKilometrageMin))
            {
                annonces = annonces.Where(s => s.Kilometrage >= intFiltreKilometrageMin);
            }

            if (!String.IsNullOrEmpty(FiltreKilometrageMax) && int.TryParse(FiltreKilometrageMax, out intFiltreKilometrageMax))
            {
                annonces = annonces.Where(s => s.Kilometrage <= intFiltreKilometrageMax);
            }

            if (!String.IsNullOrEmpty(FiltrePrixMin) && int.TryParse(FiltrePrixMin, out intFiltrePrixMin))
            {
                annonces = annonces.Where(s => s.Prix >= intFiltrePrixMin);
            }

            if (!String.IsNullOrEmpty(FiltrePrixMax) && int.TryParse(FiltrePrixMax, out intFiltrePrixMax))
            {
                annonces = annonces.Where(s => s.Prix <= intFiltrePrixMax);
            }

            if (!String.IsNullOrEmpty(FiltreCylindreeMin) && int.TryParse(FiltreCylindreeMin, out intFiltreCylindreeMin))
            {
                annonces = annonces.Where(s => s.MotoProposee.Cylindree >= intFiltreCylindreeMin);
            }

            if (!String.IsNullOrEmpty(FiltreCylindreeMax) && int.TryParse(FiltreCylindreeMax, out intFiltreCylindreeMax))
            {
                annonces = annonces.Where(s => s.MotoProposee.Cylindree <= intFiltreCylindreeMax);
            }

            if (RegionsID != null && RegionsID.Any())
            {
                annonces = annonces.Where(s => RegionsID.Contains(s.Departement.Region.ID));
            }

            if (DepartementsID != null && DepartementsID.Any())
            {
                annonces = annonces.Where(s => DepartementsID.Contains(s.Departement.ID));
            }


            if (!String.IsNullOrEmpty(FiltreIdMaMoto) && int.TryParse(FiltreIdMaMoto, out intFiltreIdMaMoto))
            {
                Moto m = db.Motos.Find(intFiltreIdMaMoto);

                annonces = annonces.ToList().Where(a =>
                    (
                        a.MotosAcceptees.Contains(m)
                        ||
                        (
                            a.MarquesAcceptees.Contains(m.Marque)
                            &&
                            a.GenresAcceptes.Contains(m.Genre)
                        )
                    )
                );
            }


            switch (sortOrder)
            {
                case "date":
                    annonces = annonces.OrderBy(s => s.Date);
                    break;
                case "annee":
                    annonces = annonces.OrderBy(s => s.Annee);
                    break;
                case "annee_desc":
                    annonces = annonces.OrderByDescending(s => s.Annee);
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
                    annonces = annonces.OrderByDescending(s => s.Date);
                    break;
            }

            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            int pageNumber = (page ?? 1);

            PopulateRegionsDropDownList(RegionsID);
            PopulateMultiDepartementsDropDownList(DepartementsID);

            ViewBag.ActionName = "Index";

            return View(annonces.ToPagedList(pageNumber, pageSize));
        }

        // Liste toutes les annonces compatibles avec une annonce donnée
        public ActionResult AnnoncesCompatibles(int id, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder == "date_desc" ? "date" : "date_desc";
            ViewBag.PrixSortParm = sortOrder == "prix" ? "prix_desc" : "prix";
            ViewBag.AnneeSortParm = sortOrder == "cylindree" ? "annee_desc" : "annee";
            ViewBag.KilometrageSortParm = sortOrder == "kilometrage" ? "kilometrage_desc" : "kilometrage";

            //var annonces = from s in db.Annonces select s;
            Annonce b = db.Annonces.Find(id);

            var annonces = db.Annonces.ToList().Where
            (a =>
                (
                    a.Autorisee == true && a.Validee == true
                )
                &&
                (
                    a.MotosAcceptees.Contains(b.MotoProposee)
                    ||
                    (
                        a.MarquesAcceptees.Contains(b.MotoProposee.Marque)
                        &&
                        a.GenresAcceptes.Contains(b.MotoProposee.Genre)
                    )
                )
                &&
                (
                    b.MotosAcceptees.Contains(a.MotoProposee)
                    ||
                    (
                        b.MarquesAcceptees.Contains(a.MotoProposee.Marque)
                        &&
                        b.GenresAcceptes.Contains(a.MotoProposee.Genre)
                    )
                )
                &&
                (
                    a.ID != id
                )
            );



            switch (sortOrder)
            {
                case "date":
                    annonces = annonces.OrderBy(s => s.Date);
                    break;
                case "annee":
                    annonces = annonces.OrderBy(s => s.Annee);
                    break;
                case "annee_desc":
                    annonces = annonces.OrderByDescending(s => s.Annee);
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
                    annonces = annonces.OrderByDescending(s => s.Date);
                    break;
            }

            ViewBag.annonceSourceId = id;
            ViewBag.ActionName = "AnnoncesCompatibles";
            
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            int pageNumber = (page ?? 1);
            return View(annonces.ToPagedList(pageNumber, pageSize));
        }

        // Liste toutes les annonces non validées
        public ActionResult AnnoncesAValider(string sortOrder, int? id)
        {
            
            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }
            
            
            var annonces = from s in db.Annonces
                           select s;

            annonces = annonces.Where(s => s.Autorisee == false && s.Validee == false);

            if (id != null)
            {
                if (id == -2)
                {
                    // On supprime les annonces non validées datant de plus de 7 jours
                    db.Annonces.RemoveRange(db.Annonces.ToList().Where(
                    a => a.Validee == false
                    &&
                    a.Autorisee == false
                    &&
                    a.Date.CompareTo(DateTime.Today.AddDays(-7)) < 0
                    ));
                }
                else if (id != -1)
                {
                    // On valide l'annonce donnée
                    Annonce annonceAValider = db.Annonces.Find(id);
                    annonceAValider.ConfirmerMotDePasse = annonceAValider.MotDePasse;
                    annonceAValider.Validee = true;
                }
                else
                {
                    // On valide toutes les annonces non validées
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

            return View("AnnoncesAValider", annonces);
        }

        // Liste toutes les annonces non autorisées et validées
        public ActionResult AnnoncesAAutoriser(string sortOrder, int? idAutoriser, int? idRefuser, string raison)
        {

            if (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"])
            {
                return RedirectToAction("PasswordAdmin", "Home");
            }

            var annonces = from s in db.Annonces
                           select s;

            annonces = annonces.Where(s => s.Autorisee == false && s.Validee == true);

            // La liste qui contiendra les mails à envoyer
            List<Email> listeMails = new List<Email>();

            if (idAutoriser != null)
            {
                if (idAutoriser != -1)
                {
                    Annonce annonceAAutoriser = db.Annonces.Find(idAutoriser);
                    annonceAAutoriser.ConfirmerMotDePasse = annonceAAutoriser.MotDePasse;
                    annonceAAutoriser.Autorisee = true;

                    listeMails.Add(new MailAnnonceAutorisee
                    {
                        Destinataire = annonceAAutoriser.Mail,
                        Nom = annonceAAutoriser.Nom,
                        Lien = Url.Action("Details", "Annonces", new { id = annonceAAutoriser.ID.ToString() }, Request.Url.Scheme)
                    });
                }
                else
                {
                    foreach (Annonce annonceAAutoriser in annonces)
                    {
                        annonceAAutoriser.ConfirmerMotDePasse = annonceAAutoriser.MotDePasse;
                        annonceAAutoriser.Autorisee = true;

                        listeMails.Add(new MailAnnonceAutorisee
                        {
                            Destinataire = annonceAAutoriser.Mail,
                            Nom = annonceAAutoriser.Nom,
                            Lien = Url.Action("Details", "Annonces", new { id = annonceAAutoriser.ID.ToString() }, Request.Url.Scheme)
                        });
                    }
                }
                db.SaveChanges();
            }
            else if (idRefuser != null)
            {
                // On supprime l'annonce
                Annonce annonceASupprimer = db.Annonces.Find(idRefuser);

                if (String.IsNullOrEmpty(raison))
                {
                    raison = "Pas de précision.";
                }

                listeMails.Add(new MailAnnonceRefusee
                {
                    Destinataire = annonceASupprimer.Mail,
                    Nom = annonceASupprimer.Nom,
                    Raison = raison
                });

                foreach (Photo photoASupprimer in annonceASupprimer.Photos)
                {
                    System.IO.File.Delete(photoASupprimer.CheminComplet);
                }

                db.Annonces.Remove(annonceASupprimer);
                db.SaveChanges();
            }

            foreach (Email mail in listeMails)
            {
                mail.Send();
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

            return View("AnnoncesAAutoriser", annonces);
        }

        // Affiche les détails d'une annonce en particulier
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

            ViewBag.ToutesMarques = annonce.MarquesAcceptees.Count() == db.Marques.Count();
            ViewBag.TousGenres = annonce.GenresAcceptes.Count() == db.Genres.Count();

            return View(annonce);
        }

        // Affiche le formulaire de création d'une annonce (premier affichage)
        public ActionResult Create()
        {

            ViewBag.tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]) / 1024;
            ViewBag.nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);
            ViewBag.nombreMaxCaracteresDescription = int.Parse(ConfigurationManager.AppSettings["nombreMaxCaracteresDescription"]);

            PopulateMotosDropDownList();
            PopulateGenresDropDownList();
            PopulateMarquesDropDownList();
            PopulateDepartementsDropDownList();

            return View();
        }

        // Affiche le formulaire de création d'une annonce (affichages suivants)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titre,Description,MotoProposeeID,PresenceMotoPerso,MotoPerso,Annee,Kilometrage,Prix,MotosAccepteesID,MarquesAccepteesID,GenresAcceptesID,Nom,Mail,Telephone,DepartementID,MotDepasse,ConfirmerMotDePasse")] Annonce annonce, IEnumerable<HttpPostedFileBase> photos)
        {

            int tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]);
            int nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);

            if (ModelState.IsValid)
            {
                
                                // On hash le mot de passe
                annonce.MotDePasse = Annonce.HashPassword(annonce.MotDePasse);
                annonce.ConfirmerMotDePasse = annonce.MotDePasse;

                // L'annonce est faite à la date du jour
                annonce.Date = DateTime.Today;
                                
                // On ajoute les modèles, marques et genres acceptés
                ProcessManyToManyRelationships(annonce);

                db.Annonces.Add(annonce);

                // On sauvi ici pour que l'ID de l'annonce soit accessible pour l'utiliser pour nommer les photos.
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

                        Image imageRedimensionneeMiniature = ScaleImage(imageOriginale, largeurMaxMiniature, hauteurMaxMiniature, false);

                        String savePathMiniature = Server.MapPath("~/Content/Photos/Miniatures/" + annonce.ID + "_" + i + ".png");

                        imageRedimensionneeMiniature.Save(savePathMiniature, System.Drawing.Imaging.ImageFormat.Png);

                        var photoMiniature = new Photo
                        {
                            Taille = Photo.TypeTaille.Miniature,
                            CheminComplet = savePathMiniature,
                            ModifiedDate = DateTime.Now
                        };

                        annonce.Photos.Add(photoMiniature);

                        // Ajout de la version Vignette de cette image
                        int largeurMaxVignette = int.Parse(ConfigurationManager.AppSettings["largeurMaxVignette"]);
                        int hauteurMaxVignette = int.Parse(ConfigurationManager.AppSettings["hauteurMaxVignette"]);

                        Image imageRedimensionneeVignette = ScaleImage(imageOriginale, largeurMaxVignette, hauteurMaxVignette, true);

                        String savePathVignette = Server.MapPath("~/Content/Photos/Vignettes/" + annonce.ID + "_" + i + ".png");

                        imageRedimensionneeVignette.Save(savePathVignette, System.Drawing.Imaging.ImageFormat.Png);

                        var photoVignette = new Photo
                        {
                            Taille = Photo.TypeTaille.Vignette,
                            CheminComplet = savePathVignette,
                            ModifiedDate = DateTime.Now
                        };

                        annonce.Photos.Add(photoVignette);

                    }
                }

                if (annonce.Photos.Count() >= 2)
                {
                    // On défini la photo principale (la première de la liste)
                    annonce.Photos.ElementAt(0).Principale = true;
                    // Et la vignette correspondante
                    annonce.Photos.ElementAt(1).Principale = true;
                }

                // On sauve à nouveau pour les modifications des images
                db.SaveChanges();

                // On envoie un mail pour valider l'adresse
                MailValiderAdresse email = new MailValiderAdresse
                {
                    Destinataire = annonce.Mail,
                    Nom = annonce.Nom,
                    Lien = Url.Action("ValidateMail", "Annonces", new { annonceId = annonce.ID.ToString(), code = annonce.CodeValidation }, Request.Url.Scheme)
                };
                
                email.Send();

                return RedirectToAction("ConfirmationAnnoncePostee");
                
            }

            ViewBag.tailleMaxiUploadEnOctet = tailleMaxiUploadEnOctet / 1024;
            ViewBag.nombreMaxdePhotos = nombreMaxdePhotos;
            ViewBag.nombreMaxCaracteresDescription = int.Parse(ConfigurationManager.AppSettings["nombreMaxCaracteresDescription"]);
            
            foreach (Moto moto in annonce.MotosAcceptees)
            {
                annonce.MotosAccepteesID.Add(moto.ID);
            }
            PopulateMotosDropDownList(annonce.MotosAccepteesID);
            
            
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
        public ActionResult EditPostPassword(int? annonceID, string password)
        {

            if (annonceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonce = db.Annonces.Find(annonceID);


            // On vérifie que le code saisi est le bon (une fois hashé)
            if (!Annonce.VerifyHashedPassword(annonce.MotDePasse, password) && (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"]))
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
            PopulateMotosDropDownList(annonce.MotosAccepteesID);

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
        public ActionResult EditPost(int? id, int? photoPrincipale, string password, IEnumerable<HttpPostedFileBase> photos)
        {

            int tailleMaxiUploadEnOctet = int.Parse(ConfigurationManager.AppSettings["tailleMaxiUploadEnOctet"]);
            int nombreMaxdePhotos = int.Parse(ConfigurationManager.AppSettings["nombreMaxdePhotos"]);
            int nombrePhotosAjoutees = 0;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonceToUpdate = db.Annonces.Find(id);


            // On vérifie que le code saisi est le bon (une fois hashé)
            if (!Annonce.VerifyHashedPassword(annonceToUpdate.MotDePasse, password) && (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"]))
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
                    
                    annonceToUpdate.Date = DateTime.Today;
                    ProcessManyToManyRelationships(annonceToUpdate);
                    db.SaveChanges();


                    if (photos != null && photos.First() != null)
                    {
                        // On vide la liste pour mettre uniquement les nouvelles photos.
                        // On supprime les photos
                        foreach (Photo photoASupprimer in annonceToUpdate.Photos)
                        {
                            System.IO.File.Delete(photoASupprimer.CheminComplet);
                        }

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

                                Image imageRedimensionneeMiniature = ScaleImage(imageOriginale, largeurMaxMiniature, hauteurMaxMiniature, false);

                                String savePathMiniature = Server.MapPath("~/Content/Photos/Miniatures/" + annonceToUpdate.ID + "_" + i + ".png");

                                imageRedimensionneeMiniature.Save(savePathMiniature, System.Drawing.Imaging.ImageFormat.Png);

                                var photoMiniature = new Photo
                                {
                                    Taille = Photo.TypeTaille.Miniature,
                                    CheminComplet = savePathMiniature,
                                    ModifiedDate = DateTime.Now
                                };

                                annonceToUpdate.Photos.Add(photoMiniature);

                                // Ajout de la version Vignette de cette image
                                int largeurMaxVignette = int.Parse(ConfigurationManager.AppSettings["largeurMaxVignette"]);
                                int hauteurMaxVignette = int.Parse(ConfigurationManager.AppSettings["hauteurMaxVignette"]);

                                Image imageRedimensionneeVignette = ScaleImage(imageOriginale, largeurMaxVignette, hauteurMaxVignette, true);

                                String savePathVignette = Server.MapPath("~/Content/Photos/Vignettes/" + annonceToUpdate.ID + "_" + i + ".png");

                                imageRedimensionneeVignette.Save(savePathVignette, System.Drawing.Imaging.ImageFormat.Png);

                                var photoVignette = new Photo
                                {
                                    Taille = Photo.TypeTaille.Vignette,
                                    CheminComplet = savePathVignette,
                                    ModifiedDate = DateTime.Now
                                };

                                annonceToUpdate.Photos.Add(photoVignette);
                                nombrePhotosAjoutees++;
                            }
                        }
                    }

                    if (nombrePhotosAjoutees >= 1)
                    {
                        // On défini la photo principale (la première de la liste)
                        annonceToUpdate.Photos.ElementAt(0).Principale = true;
                        // Et la vignette correspondante
                        annonceToUpdate.Photos.ElementAt(1).Principale = true;
                    }
                    else if (photoPrincipale != null && photoPrincipale != 0)
                    {
                        // On change de photo principale s'il y a des photos et s'il y a un changement de photo principale
                        foreach (Photo photoP in annonceToUpdate.Photos)
                        {
                            photoP.Principale = false;
                        }

                        Photo photoPr = annonceToUpdate.Photos.Find(p => p.ID == photoPrincipale);
                        photoPr.Principale = true;
                        annonceToUpdate.Photos.ElementAt(annonceToUpdate.Photos.IndexOf(photoPr) + 1).Principale = true;
                    }

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
            PopulateMotosDropDownList(annonceToUpdate.MotosAccepteesID);



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

            ViewBag.password = password;

            return View("Edit", annonceToUpdate);

        }

        // Affiche la demande de mot de passe avant de pouvoir supprimer l'annonce
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
        public ActionResult DeletePostPassword(int? annonceID, string password)
        {

            if (annonceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var annonce = db.Annonces.Find(annonceID);

            
            // On vérifie que le code saisi est le bon (une fois hashé)
            if (!Annonce.VerifyHashedPassword(annonce.MotDePasse, password) && (Session["estAdmin"] == null || !(Boolean)Session["estAdmin"]))
            {
                ViewBag.Message = "Mot de passe incorrect";
                ViewBag.AnnonceID = annonce.ID;
                ViewBag.actionName = "DeletePassword";
                ViewBag.supprimee = false;
                return View("Delete");
            }

            // A ce niveau, le mot de passe a été renseigné et est correct
            // On supprime les photos
            foreach (Photo photoASupprimer in annonce.Photos)
            {
                System.IO.File.Delete(photoASupprimer.CheminComplet);
            }
                        
            // On supprime l'annonce
            db.Annonces.Remove(annonce);
            db.SaveChanges();


            ViewBag.supprimee = true;
            return View("Delete");

        }

        // Affiche la page de validation de l'adresse (et donc de l'annonce) si le code fourni est le bon
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

            // Uniquement si l'annonce n'est pas déjà validée.
            if (!annonce.Validee)
            {

                if (!code.Equals(annonce.CodeValidation))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


                annonce.ConfirmerMotDePasse = annonce.MotDePasse;
                annonce.Validee = true;

                db.SaveChanges();

                // On envoie un mail à l'admin
                MailAdminAnnoncePostee email = new MailAdminAnnoncePostee
                {
                    Lien = Url.Action("Details", "Annonces", new { id = annonce.ID.ToString() }, Request.Url.Scheme)
                };

                email.Send();
            }
            
            return View();
        }

        // Affiche la page de confirmation après avoir posté une annonce
        public ActionResult ConfirmationAnnoncePostee()
        {
            return View();
        }

        // Permet d'envoyer un mail à une annonce en passant par le site
        public ActionResult EnvoyerMailAnnonce(int id)
        {
            Annonce annonce = db.Annonces.Find(id);

            return View(annonce);
        }

        [HttpPost, ActionName("EnvoyerMailAnnonce")]
        [ValidateAntiForgeryToken]
        public ActionResult EnvoyerMailAnnoncePost(int id, string mailExpediteur, string message)
        {
            Annonce annonce = db.Annonces.Find(id);

            if (String.IsNullOrEmpty(mailExpediteur) || String.IsNullOrEmpty(message))
            {
                ViewBag.MessageErreur = "Les données sont incorrectes.";
                return View(annonce);
            }

            // On envoie un mail pour valider l'adresse
            MailEnvoyer email = new MailEnvoyer
            {
                Destinataire = annonce.Mail,
                TitreAnnonce = annonce.Titre,
                Expediteur = mailExpediteur,
                Message = message
            };

            try
            {
                email.Send();
            }
            catch (Exception)
            {
                ViewBag.MessageErreur = "Adresse mail non valide (domaine inconnu).";
                return View("EnvoyerMailAnnonce", annonce);
            }

            return RedirectToAction("MessageEnvoye");
        }

        // Affiche la page de confirmation après avoir envoyé un message
        public ActionResult MessageEnvoye()
        {
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
        private void PopulateMotosDropDownList(List<int> selectedMotos = null)
        {
            var motosQuery = from d in db.Motos
                              orderby d.Marque.Nom, d.Modele
                              select d;

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

        // Peuple la liste déroulante des départements
        private void PopulateMultiDepartementsDropDownList(List<int> selectedDepartements = null)
        {
            var departementsQuery = from d in db.Departements
                                    orderby d.Nom
                                    select d;

            ViewBag.DepartementsID = new MultiSelectList(departementsQuery, "ID", "Nom", selectedDepartements);
        }

        // Peuple la liste déroulante des régions
        private void PopulateRegionsDropDownList(List<int> selectedRegions = null)
        {
            var regionsQuery = from d in db.Regions
                               orderby d.Nom
                               select d;

            ViewBag.RegionsID = new MultiSelectList(regionsQuery, "ID", "Nom", selectedRegions);

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

            // On affine les listes
            // S'il y a des marques mais pas de genres, on considère que tous les genres sont acceptés.
            // Et inversement
            if (annonce.MarquesAcceptees.Any() && !annonce.GenresAcceptes.Any())
            {
                annonce.GenresAcceptes.AddRange(db.Genres.ToList());
            }
            if (!annonce.MarquesAcceptees.Any() && annonce.GenresAcceptes.Any())
            {
                annonce.MarquesAcceptees.AddRange(db.Marques.ToList());
            }
            
        }

        // Redimensionne l'image en gardant son ratio et en étant inférieur au maximum de largeur et hauteur
        private Image ScaleImage(Image image, int maxWidth, int maxHeight, Boolean watermarker)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);


            string watermarkText = "© moto-echange.fr";

            if (watermarker)
            { 
                using (Graphics grp = Graphics.FromImage(newImage))
                {

                    //Set the Color of the Watermark text.
                    Brush brush = new SolidBrush(Color.Red);

                    //Set the Font and its size.
                    Font font = new System.Drawing.Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel);

                    //Determine the size of the Watermark text.
                    SizeF textSize = new SizeF();
                    textSize = grp.MeasureString(watermarkText, font);

                    //Position the text and draw it on the image.
                    Point position = new Point((newImage.Width - ((int)textSize.Width + 10)), (newImage.Height - ((int)textSize.Height + 10)));
                    grp.DrawString(watermarkText, font, brush, position);

                    
                }
            }
            


            return newImage;
        }        
              
        // Récupérer le type Mime de l'image
        private string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }

    }
}
