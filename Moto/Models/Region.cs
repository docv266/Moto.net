using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Region
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Nom { get; set; }

        public virtual List<Departement> Departements { get; set; }
    }
}