using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Annonce
    {
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Titre { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(1500, 3000)]
        [DataType(DataType.Text)]
        public int Annee { get; set; }

        [Required]
        [Range(0, 99999)]
        [DataType(DataType.Text)]
        public int Kilometrage { get; set; }

        [Required]
        [Range(0, 999999)]
        [DataType(DataType.Currency)]
        public int Prix { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [Required]
        public string Date { get; set; }

        public Boolean Autorisee { get; set; }

        public Boolean Validee { get; set; }

        [Required]
        public int MotoProposeeID { get; set; }

        [Required]
        public int DepartementID { get; set; }

        public List<int> MotosAccepteesID { get; set; }

        public List<int> MarquesAccepteesID { get; set; }

        public List<int> GenresAcceptesID { get; set; }


        public virtual Moto MotoProposee { get; set; }
        public virtual List<Photo> Photos { get; set; }
        public virtual List<Moto> MotosAcceptees { get; set; }
        public virtual List<Marque> MarquesAcceptees { get; set; }
        public virtual List<Genre> GenresAcceptes { get; set; }
        public virtual Departement Departement { get; set; }
    }
}