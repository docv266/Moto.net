using Motonet.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motonet.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class PhotoController : Controller
    {
        private MotoContext db = new MotoContext();

        // GET: Photo
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Photos.Find(id);

            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}