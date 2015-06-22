using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moto.Models
{
    public class Departement
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public int CP { get; set; }

        public virtual Region Region { get; set; }
        public virtual List<Annonce> Annonces { get; set; }
    }
}