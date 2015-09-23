using Motonet.DAL;
using Motonet.Models;
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
            Photo fileToRetrieve = db.Photos.Find(id);

            return File(fileToRetrieve.CheminComplet, GetContentType(fileToRetrieve.CheminComplet));
        }

        private string GetContentType(string fileName)
        {
            string strcontentType = "application/octet-stream";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                strcontentType = registryKey.GetValue("Content Type").ToString();
            return strcontentType;
        }
    }
}