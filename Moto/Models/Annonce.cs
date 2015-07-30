using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motonet.Models
{
    public class Annonce
    {
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int Annee { get; set; }
        public int Kilometrage { get; set; }
        public int Prix { get; set; }
        public string Nom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string MotDePasse { get; set; }
        public string Date { get; set; }
        public Boolean Autorisee { get; set; }
        public Boolean Validee { get; set; }


        public virtual Moto Moto { get; set; }
        public virtual List<Photo> Photos { get; set; }
        public virtual List<Moto> MotosAcceptees { get; set; }
        public virtual List<Marque> MarquesAcceptees { get; set; }
        public virtual List<Genre> GenresAcceptes { get; set; }
        public virtual Departement Departement { get; set; }
    }
}