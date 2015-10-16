using Motonet.DAL;
using Motonet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Motonet.Controllers
{
    public class HomeController : Controller
    {
        private MotoContext db = new MotoContext();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult MentionsLegales()
        {
            return View();
        }

        public ActionResult GenericError()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error403()
        {
            return View();
        }

        public ActionResult PasswordAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordAdminPost(string password)
        {
            Settings setting = db.Settings.FirstOrDefault();

            if (setting != null)
            {

                Session["estAdmin"] = !string.IsNullOrEmpty(password) && Annonce.VerifyHashedPassword(setting.AdminPassword, password);

                if ((Boolean)Session["estAdmin"])
                {
                    return RedirectToAction("AnnoncesAAutoriser", "Annonces");
                }
            }

            ViewBag.Message = "Mot de passe incorrect.";
            return View("PasswordAdmin");

            
        }

    }
}