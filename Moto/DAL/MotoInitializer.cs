using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Motonet.Models;

namespace Motonet.DAL
{
    public class MotoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MotoContext>
    {
        protected override void Seed(MotoContext context)
        {
            var regions = new List<Region>
            {
                new Region{Nom="Alsace"},
                new Region{Nom="Aquitaine"},
                new Region{Nom="Auvergne"},
                new Region{Nom="Basse-Normandie"},
                new Region{Nom="Bourgogne"},
                new Region{Nom="Bretagne"},
                new Region{Nom="Centre-Val de Loire"},
                new Region{Nom="Champagne-Ardenne"},
                new Region{Nom="Corse"},
                new Region{Nom="Franche-Comté"},
                new Region{Nom="Guadeloupe"},
                new Region{Nom="Guyane"},
                new Region{Nom="Haute-Normandie"},
                new Region{Nom="Ile-de-France"},
                new Region{Nom="Languedoc-Roussillon"},
                new Region{Nom="Limousin"},
                new Region{Nom="Lorraine"},
                new Region{Nom="Martinique"},
                new Region{Nom="Mayotte"},
                new Region{Nom="Midi-Pyrénées"},
                new Region{Nom="Nord-Pas-de-Calais"},
                new Region{Nom="Pays de la Loire"},
                new Region{Nom="Picardie"},
                new Region{Nom="Poitou-Charentes"},
                new Region{Nom="Provence-Alpes-Côte d'Azur"},
                new Region{Nom="Réunion"},
                new Region{Nom="Rhône-Alpes"}
            };

            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var departements = new List<Departement>
            {
                new Departement{Nom="Bas Rhin",CP="67",Region=regions.Find(r => r.Nom.Equals("Alsace"))},
                new Departement{Nom="Haut Rhin",CP="68",Region=regions.Find(r => r.Nom.Equals("Alsace"))},
                new Departement{Nom="Dordogne",CP="24",Region=regions.Find(r => r.Nom.Equals("Aquitaine"))},
                new Departement{Nom="Gironde",CP="33",Region=regions.Find(r => r.Nom.Equals("Aquitaine"))},
                new Departement{Nom="Landes",CP="40",Region=regions.Find(r => r.Nom.Equals("Aquitaine"))},
                new Departement{Nom="Lot et Garonne",CP="47",Region=regions.Find(r => r.Nom.Equals("Aquitaine"))},
                new Departement{Nom="Pyrénées Atlantiques",CP="64",Region=regions.Find(r => r.Nom.Equals("Aquitaine"))},
                new Departement{Nom="Allier",CP="03",Region=regions.Find(r => r.Nom.Equals("Auvergne"))},
                new Departement{Nom="Cantal",CP="15",Region=regions.Find(r => r.Nom.Equals("Auvergne"))},
                new Departement{Nom="Haute Loire",CP="43",Region=regions.Find(r => r.Nom.Equals("Auvergne"))},
                new Departement{Nom="Puy de Dôme",CP="63",Region=regions.Find(r => r.Nom.Equals("Auvergne"))},
                new Departement{Nom="Calvados",CP="14",Region=regions.Find(r => r.Nom.Equals("Basse-Normandie"))},
                new Departement{Nom="Manche",CP="50",Region=regions.Find(r => r.Nom.Equals("Basse-Normandie"))},
                new Departement{Nom="Orne",CP="61",Region=regions.Find(r => r.Nom.Equals("Basse-Normandie"))},
                new Departement{Nom="Côte d Or",CP="21",Region=regions.Find(r => r.Nom.Equals("Bourgogne"))},
                new Departement{Nom="Nièvre",CP="58",Region=regions.Find(r => r.Nom.Equals("Bourgogne"))},
                new Departement{Nom="Saône et Loire",CP="71",Region=regions.Find(r => r.Nom.Equals("Bourgogne"))},
                new Departement{Nom="Yonne",CP="89",Region=regions.Find(r => r.Nom.Equals("Bourgogne"))},
                new Departement{Nom="Côte d Armor",CP="22",Region=regions.Find(r => r.Nom.Equals("Bretagne"))},
                new Departement{Nom="Finistère",CP="29",Region=regions.Find(r => r.Nom.Equals("Bretagne"))},
                new Departement{Nom="Ille et Vilaine",CP="35",Region=regions.Find(r => r.Nom.Equals("Bretagne"))},
                new Departement{Nom="Morbihan",CP="56",Region=regions.Find(r => r.Nom.Equals("Bretagne"))},
                new Departement{Nom="Cher",CP="18",Region=regions.Find(r => r.Nom.Equals("Centre"))},
                new Departement{Nom="Eure et Loire",CP="28",Region=regions.Find(r => r.Nom.Equals("Centre"))},
                new Departement{Nom="Indre",CP="36",Region=regions.Find(r => r.Nom.Equals("Centre"))},
                new Departement{Nom="Indre et Loire",CP="37",Region=regions.Find(r => r.Nom.Equals("Centre"))},
                new Departement{Nom="Loire et Cher",CP="41",Region=regions.Find(r => r.Nom.Equals("Centre"))},
                new Departement{Nom="Loiret",CP="45",Region=regions.Find(r => r.Nom.Equals("Centre"))},
                new Departement{Nom="Ardennes",CP="08",Region=regions.Find(r => r.Nom.Equals("Champagne-Ardenne"))},
                new Departement{Nom="Aube",CP="10",Region=regions.Find(r => r.Nom.Equals("Champagne-Ardenne"))},
                new Departement{Nom="Marne",CP="51",Region=regions.Find(r => r.Nom.Equals("Champagne-Ardenne"))},
                new Departement{Nom="Haute Marne",CP="52",Region=regions.Find(r => r.Nom.Equals("Champagne-Ardenne"))},
                new Departement{Nom="Corse du Sud",CP="2A",Region=regions.Find(r => r.Nom.Equals("Corse"))},
                new Departement{Nom="Haute Corse",CP="2B",Region=regions.Find(r => r.Nom.Equals("Corse"))},
                new Departement{Nom="Doubs",CP="25",Region=regions.Find(r => r.Nom.Equals("Franche-Comté"))},
                new Departement{Nom="Jura",CP="39",Region=regions.Find(r => r.Nom.Equals("Franche-Comté"))},
                new Departement{Nom="Haute Saône",CP="70",Region=regions.Find(r => r.Nom.Equals("Franche-Comté"))},
                new Departement{Nom="Territoire de Belfort",CP="90",Region=regions.Find(r => r.Nom.Equals("Franche-Comté"))},
                new Departement{Nom="Eure",CP="27",Region=regions.Find(r => r.Nom.Equals("Haute-Normandie"))},
                new Departement{Nom="Seine Maritime",CP="76",Region=regions.Find(r => r.Nom.Equals("Haute-Normandie"))},
                new Departement{Nom="Paris",CP="75",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Seine et Marne",CP="77",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Yvelines",CP="78",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Essonne",CP="91",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Hauts de Seine",CP="92",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Seine Saint Denis",CP="93",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Val de Marne",CP="94",Region=regions.Find(r => r.Nom.Equals("Ile-de-France"))},
                new Departement{Nom="Aude",CP="11",Region=regions.Find(r => r.Nom.Equals("Languedoc-Roussillon"))},
                new Departement{Nom="Gard",CP="30",Region=regions.Find(r => r.Nom.Equals("Languedoc-Roussillon"))},
                new Departement{Nom="Hérault",CP="34",Region=regions.Find(r => r.Nom.Equals("Languedoc-Roussillon"))},
                new Departement{Nom="Lozère",CP="48",Region=regions.Find(r => r.Nom.Equals("Languedoc-Roussillon"))},
                new Departement{Nom="Pyrénées Orientales",CP="66",Region=regions.Find(r => r.Nom.Equals("Languedoc-Roussillon"))},
                new Departement{Nom="Corrèze",CP="19",Region=regions.Find(r => r.Nom.Equals("Limousin"))},
                new Departement{Nom="Creuse",CP="23",Region=regions.Find(r => r.Nom.Equals("Limousin"))},
                new Departement{Nom="Haute Vienne",CP="87",Region=regions.Find(r => r.Nom.Equals("Limousin"))},
                new Departement{Nom="Meurthe et Moselle",CP="54",Region=regions.Find(r => r.Nom.Equals("Lorraine"))},
                new Departement{Nom="Meuse",CP="55",Region=regions.Find(r => r.Nom.Equals("Lorraine"))},
                new Departement{Nom="Moselle",CP="57",Region=regions.Find(r => r.Nom.Equals("Lorraine"))},
                new Departement{Nom="Vosges",CP="88",Region=regions.Find(r => r.Nom.Equals("Lorraine"))},
                new Departement{Nom="Ariège",CP="09",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Aveyron",CP="12",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Haute Garonne",CP="31",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Gers",CP="32",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Lot",CP="46",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Hautes Pyrénées",CP="65",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Tarn",CP="81",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Tarn et Garonne",CP="82",Region=regions.Find(r => r.Nom.Equals("Midi-Pyrénées"))},
                new Departement{Nom="Nord",CP="59",Region=regions.Find(r => r.Nom.Equals("Nord-Pas-de-Calais"))},
                new Departement{Nom="Pas de Calais",CP="62",Region=regions.Find(r => r.Nom.Equals("Nord-Pas-de-Calais"))},
                new Departement{Nom="Guadeloupe",CP="971",Region=regions.Find(r => r.Nom.Equals("Outre-Mer"))},
                new Departement{Nom="Martinique",CP="972",Region=regions.Find(r => r.Nom.Equals("Outre-Mer"))},
                new Departement{Nom="Guyane",CP="973",Region=regions.Find(r => r.Nom.Equals("Outre-Mer"))},
                new Departement{Nom="La Réunion",CP="974",Region=regions.Find(r => r.Nom.Equals("Outre-Mer"))},
                new Departement{Nom="Mayotte",CP="976",Region=regions.Find(r => r.Nom.Equals("Outre-Mer"))},
                new Departement{Nom="Loire Atlantique",CP="44",Region=regions.Find(r => r.Nom.Equals("Pays de la Loire"))},
                new Departement{Nom="Maine et Loire",CP="49",Region=regions.Find(r => r.Nom.Equals("Pays de la Loire"))},
                new Departement{Nom="Mayenne",CP="53",Region=regions.Find(r => r.Nom.Equals("Pays de la Loire"))},
                new Departement{Nom="Sarthe",CP="72",Region=regions.Find(r => r.Nom.Equals("Pays de la Loire"))},
                new Departement{Nom="Vendée",CP="85",Region=regions.Find(r => r.Nom.Equals("Pays de la Loire"))},
                new Departement{Nom="Aisne",CP="02",Region=regions.Find(r => r.Nom.Equals("Picardie"))},
                new Departement{Nom="Oise",CP="60",Region=regions.Find(r => r.Nom.Equals("Picardie"))},
                new Departement{Nom="Somme",CP="80",Region=regions.Find(r => r.Nom.Equals("Picardie"))},
                new Departement{Nom="16",CP="Charente",Region=regions.Find(r => r.Nom.Equals("Poitou-Charentes"))},
                new Departement{Nom="17",CP="Charente Maritime",Region=regions.Find(r => r.Nom.Equals("Poitou-Charentes"))},
                new Departement{Nom="79",CP="Deux Sèvres",Region=regions.Find(r => r.Nom.Equals("Poitou-Charentes"))},
                new Departement{Nom="86",CP="Vienne",Region=regions.Find(r => r.Nom.Equals("Poitou-Charentes"))},
                new Departement{Nom="Alpes",CP="04",Region=regions.Find(r => r.Nom.Equals("Provence-Alpes-Côte d'Azur"))},
                new Departement{Nom="Hautes Alpes",CP="05",Region=regions.Find(r => r.Nom.Equals("Provence-Alpes-Côte d'Azur"))},
                new Departement{Nom="Alpes Maritimes",CP="06",Region=regions.Find(r => r.Nom.Equals("Provence-Alpes-Côte d'Azur"))},
                new Departement{Nom="Bouches du Rhône",CP="13",Region=regions.Find(r => r.Nom.Equals("Provence-Alpes-Côte d'Azur"))},
                new Departement{Nom="Var",CP="83",Region=regions.Find(r => r.Nom.Equals("Provence-Alpes-Côte d'Azur"))},
                new Departement{Nom="Vaucluse",CP="84",Region=regions.Find(r => r.Nom.Equals("Provence-Alpes-Côte d'Azur"))},
                new Departement{Nom="Ain",CP="01",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Ardèche",CP="07",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Drôme",CP="26",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Isère",CP="38",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Loire",CP="42",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Rhône",CP="69",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Savoie ",CP="73",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))},
                new Departement{Nom="Haute Savoie",CP="74",Region=regions.Find(r => r.Nom.Equals("Rhône-Alpes"))}
            };
            departements.ForEach(s => context.Departements.Add(s));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre{Nom="Roadster"},
                new Genre{Nom="Trail"},
                new Genre{Nom="GT"},
                new Genre{Nom="Routière"},
                new Genre{Nom="Sportive"},
                new Genre{Nom="Custom"},
                new Genre{Nom="Supermotard"},
                new Genre{Nom="Scooter"},
                new Genre{Nom="Cross/Enduro/Trial"}
            };
            genres.ForEach(s => context.Genres.Add(s));
            context.SaveChanges();

            var marques = new List<Marque>
            {
                new Marque{Nom="Ducati"},
                new Marque{Nom="BMW"},
                new Marque{Nom="Triumph"},
                new Marque{Nom="KTM"},
                new Marque{Nom="Aprilia"},
                new Marque{Nom="Moto Guzzi"},
                new Marque{Nom="MV Agusta"},
                new Marque{Nom="Harley-Davidson"},
                new Marque{Nom="Victory"},
                new Marque{Nom="Indian"},
                new Marque{Nom="Honda"},
                new Marque{Nom="Yamaha"},
                new Marque{Nom="Suzuki"},
                new Marque{Nom="Kawasaki"}
            };
            marques.ForEach(s => context.Marques.Add(s));
            context.SaveChanges();

            var motos = new List<Moto>
            {
                new Moto{Modele="Monster", Cylindree=1100, MarqueID=marques.Find(r => r.Nom.Equals("Ducati")).ID, GenreID=genres.Find(r => r.Nom.Equals("Roadster")).ID},
                new Moto{Modele="Monster", Cylindree=800, MarqueID=marques.Find(r => r.Nom.Equals("Ducati")).ID, GenreID=genres.Find(r => r.Nom.Equals("Roadster")).ID},
                new Moto{Modele="Hypermotard", Cylindree=1100, MarqueID=marques.Find(r => r.Nom.Equals("Ducati")).ID, GenreID=genres.Find(r => r.Nom.Equals("Supermotard")).ID},
                new Moto{Modele="CBF", Cylindree=1000, MarqueID=marques.Find(r => r.Nom.Equals("Honda")).ID, GenreID=genres.Find(r => r.Nom.Equals("Routière")).ID},
                new Moto{Modele="CBR", Cylindree=1000, MarqueID=marques.Find(r => r.Nom.Equals("Honda")).ID, GenreID=genres.Find(r => r.Nom.Equals("Roadster")).ID}
            };
            motos.ForEach(s => context.Motos.Add(s));
            context.SaveChanges();
        }
    }
}