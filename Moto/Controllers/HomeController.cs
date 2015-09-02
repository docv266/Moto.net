using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motonet.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GenericError()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult InternalError()
        {
            return View();
        }
    }
}