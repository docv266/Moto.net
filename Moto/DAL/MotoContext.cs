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

        public MotoContext()
            : base("MyConnection")
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

            modelBuilder.Entity<Moto>().HasRequired(m => m.Genre).WithMany(g => g.Motos).WillCascadeOnDelete(false);
            modelBuilder.Entity<Moto>().HasRequired(m => m.Marque).WithMany(m => m.Motos).WillCascadeOnDelete(false);

            modelBuilder.Entity<Departement>().HasRequired(d => d.Region).WithMany(r => r.Departements).WillCascadeOnDelete(false);

            modelBuilder.Entity<Annonce>().HasRequired(d => d.MotoProposee).WithMany(m => m.AnnoncesAvecMotoProposee).WillCascadeOnDelete(false);
            modelBuilder.Entity<Annonce>().HasRequired(d => d.Departement).WithMany(d => d.Annonces).WillCascadeOnDelete(false);

            modelBuilder.Entity<Annonce>().HasMany(a => a.MarquesAcceptees).WithMany(m => m.Annonces);
            modelBuilder.Entity<Annonce>().HasMany(a => a.MotosAcceptees).WithMany(m => m.AnnoncesAvecMotosAcceptees);
            modelBuilder.Entity<Annonce>().HasMany(a => a.GenresAcceptes).WithMany(g => g.Annonces);

            modelBuilder.Entity<Annonce>().HasMany(a => a.Photos).WithRequired(p => p.Annonce);

            
        }
    }
}