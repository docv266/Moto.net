using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motonet.Models
{
    public class Moto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [DisplayAttribute(Name = "Modèle")]
        public string Modele { get; set; }

        [Required]
        [DisplayAttribute(Name = "Marque")]
        public int MarqueID { get; set; }

        [Required]
        [DisplayAttribute(Name = "Genre")]
        public int GenreID { get; set; }

        [Required]
        [DisplayAttribute(Name = "Cylindrée")]
        public int Cylindree { get; set; }

        [Required]
        public virtual Marque Marque { get; set; }

        [Required]
        public virtual Genre Genre { get; set; }

        [InverseProperty("MotoProposee")]
        public virtual List<Annonce> AnnoncesAvecMotoProposee { get; set; }

        [InverseProperty("MotosAcceptees")]
        public virtual List<Annonce> AnnoncesAvecMotosAcceptees { get; set; }

        public string Identification
        {
            get
            {
                return Marque.Nom + " " + Modele + " " + Cylindree;
            }
        }
    }
}