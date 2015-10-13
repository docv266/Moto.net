using Motonet.DAL;
using Motonet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motonet.ViewModels
{
    public class CreateEditViewModel
    {
        public CreateEditViewModel()
        {
            Annonce = new Annonce();

            MotoProposeeList = new SelectList(new List<String>(), "ID", "Identification", null);

            MotosAccepteesList = new MultiSelectList(new List<String>(), "ID", "Identification", null);

            MarquesAccepteesList = new MultiSelectList(new List<String>(), "ID", "Identification", null);

            GenresAcceptesList = PopulateGenresDropDownList();

            DepartementList = new SelectList(new List<String>(), "ID", "Identification", null);
        }

        public bool CreatePage { get; set; }

        public Annonce Annonce { get; set; }

        public int tailleMaxiUploadEnOctet { get; set; }
        public int nombreMaxdePhotos { get; set; }
        public int nombreMaxCaracteresDescription { get; set; }

        public SelectList MotoProposeeList { get; set; }

        public MultiSelectList MotosAccepteesList { get; set; }

        public MultiSelectList MarquesAccepteesList { get; set; }

        public MultiSelectList GenresAcceptesList { get; set; }

        public SelectList DepartementList { get; set; }

        private MultiSelectList PopulateGenresDropDownList(List<int> selectedGenres = null)
        {
            MotoContext db = new MotoContext();

            var genresQuery = from d in db.Genres
                              orderby d.Nom
                              select d;

            return new MultiSelectList(genresQuery, "ID", "Nom", selectedGenres);
        }
    }

}

