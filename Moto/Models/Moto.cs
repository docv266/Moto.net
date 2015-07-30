using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motonet.Models
{
    public class Moto
    {
        public int ID { get; set; }
        public string Modele { get; set; }
        public Marque Marque { get; set; }
        public Genre Genre { get; set; }
        public int Cylindree { get; set; }

        public virtual List<Annonce> Annonces { get; set; }
    }
}