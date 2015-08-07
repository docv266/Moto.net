using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motonet.Models
{
    public class Annonce
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, MinimumLength = 3)]
        public string Titre { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }
        
        [Required]
        [Range(1500, 3000, ErrorMessage = "Veuillez saisir un nombre compris entre 1500 et 3000.")]
        [DisplayAttribute(Name="Année")]
        public int Annee { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "Veuillez saisir un nombre compris entre 0 et 99999.")]
        [DisplayAttribute(Name = "Kilomètrage")]
        public int Kilometrage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 999999, ErrorMessage = "Veuillez saisir un nombre compris entre 0 et 999999.")]
        public int Prix { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayAttribute(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayAttribute(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Boolean Autorisee { get; set; }

        public Boolean Validee { get; set; }

        [Required]
        [DisplayAttribute(Name = "Modèle")]
        public int MotoProposeeID { get; set; }

        [Required]
        [DisplayAttribute(Name = "Département")]
        public int DepartementID { get; set; }

        [DisplayAttribute(Name = "Modèles")]
        public List<int> MotosAccepteesID { get; set; }

        [DisplayAttribute(Name = "Marques")]
        public List<int> MarquesAccepteesID { get; set; }

        [DisplayAttribute(Name = "Genres")]
        public List<int> GenresAcceptesID { get; set; }


        [InverseProperty("AnnoncesAvecMotoProposee")]
        public virtual Moto MotoProposee { get; set; }

        public virtual Departement Departement { get; set; }

        public virtual List<Photo> Photos { get; set; }

        [InverseProperty("AnnoncesAvecMotosAcceptees")]
        public virtual List<Moto> MotosAcceptees { get; set; }

        public virtual List<Marque> MarquesAcceptees { get; set; }

        public virtual List<Genre> GenresAcceptes { get; set; }
    }
}