using Motonet.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motonet.Controllers
{
    public class DepartementsController : Controller
    {
        public ActionResult ListePartielleDepartements(string q)
        {
            MotoContext mc = new MotoContext();

            if (String.IsNullOrEmpty(q))
            {
                return null;
            }

            var suggestions = from s in mc.Departements
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

        public ActionResult DepartementEnParticulier(string q)
        {
            MotoContext mc = new MotoContext();

            if (String.IsNullOrEmpty(q))
            {
                return null;
            }

            var suggestions = from s in mc.Departements
                              select new
                              {
                                  id = s.ID,
                                  value = s.Nom
                              };

            List<int> liste = q.Split('/').Select(Int32.Parse).ToList();

            var motoList = suggestions.ToList().Where(n => liste.Contains(n.id));

            return Json(motoList.Select(m => new
            {
                id = m.id,
                text = m.value
            }), JsonRequestBehavior.AllowGet);

        }

    }
        
}