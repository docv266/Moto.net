using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Genre
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Nom { get; set; }

        public virtual List<Moto> Motos { get; set; }
        public virtual List<Annonce> Annonces { get; set; }
    }
}