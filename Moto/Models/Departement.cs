using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Departement
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string CP { get; set; }

        [Required]
        public virtual Region Region { get; set; }

        public virtual List<Annonce> Annonces { get; set; }
    }
}