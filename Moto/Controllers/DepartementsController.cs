using Motonet.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
            var motoList = suggestions.ToList().Where(n => RetirerAccent(n.value).ToUpper().Contains(q.ToUpper())).Take(20);

           
            return Json(motoList.Select(m => new
            {
                id = m.id,
                text = m.value
            }), JsonRequestBehavior.AllowGet);

        }

        private String RetirerAccent(String chaine)
        {
            var normalizedString = chaine.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
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