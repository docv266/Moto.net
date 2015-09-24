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

            HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
            HttpContext.Response.Cache.SetMaxAge(new TimeSpan(1, 0, 0));

            Photo fileToRetrieve = db.Photos.Find(id);

            string rawIfModifiedSince = HttpContext.Request.Headers.Get("If-Modified-Since");
            if (string.IsNullOrEmpty(rawIfModifiedSince))
            {
                // Set Last Modified time
                HttpContext.Response.Cache.SetLastModified(fileToRetrieve.ModifiedDate);
            }
            else
            {
                DateTime ifModifiedSince = DateTime.Parse(rawIfModifiedSince);


                // HTTP does not provide milliseconds, so remove it from the comparison
                if (fileToRetrieve.ModifiedDate.AddMilliseconds(-fileToRetrieve.ModifiedDate.Millisecond) == ifModifiedSince)
                {
                    // The requested file has not changed
                    HttpContext.Response.StatusCode = 304;
                    return Content(string.Empty);
                }
            }

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