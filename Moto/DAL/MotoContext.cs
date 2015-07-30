using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Motonet.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Motonet.DAL
{
    public class MotoContext: DbContext
    {
    
        public MotoContext() : base("MotoContext")
        {
        }
        
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}