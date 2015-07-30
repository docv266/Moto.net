using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motonet.Models
{
    public class Region
    {
        public int ID { get; set; }
        public string Nom { get; set; }

        public virtual List<Departement> Departements { get; set; }
    }
}