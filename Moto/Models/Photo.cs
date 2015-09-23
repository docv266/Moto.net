using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Photo
    {
        public int ID { get; set; }

        public TypeTaille Taille { get; set; }

        public string CheminComplet { get; set; }

        public int AnnonceID { get; set; }

        public Boolean Principale { get; set; }

        public virtual Annonce Annonce { get; set; }

        public enum TypeTaille { Miniature, Vignette }
    }

}