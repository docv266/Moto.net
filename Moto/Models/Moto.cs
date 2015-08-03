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
        public int MarqueID { get; set; }
        public int GenreID { get; set; }
        public int Cylindree { get; set; }

        public virtual List<Annonce> Annonces { get; set; }
    }
}