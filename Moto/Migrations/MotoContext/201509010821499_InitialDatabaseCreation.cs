namespace Motonet.Migrations.MotoContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annonce",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Titre = c.String(nullable: false, maxLength: 60),
                        Description = c.String(nullable: false),
                        Annee = c.Int(nullable: false),
                        Kilometrage = c.Int(nullable: false),
                        Prix = c.Int(nullable: false),
                        Nom = c.String(nullable: false, maxLength: 30),
                        MotDePasse = c.String(nullable: false, maxLength: 68),
                        CodeValidation = c.String(nullable: false, maxLength: 20),
                        Mail = c.String(nullable: false, maxLength: 50),
                        Telephone = c.String(),
                        Date = c.DateTime(nullable: false),
                        Autorisee = c.Boolean(nullable: false),
                        Validee = c.Boolean(nullable: false),
                        MotoProposeeID = c.Int(nullable: false),
                        DepartementID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departement", t => t.DepartementID)
                .ForeignKey("dbo.Moto", t => t.MotoProposeeID)
                .Index(t => t.MotoProposeeID)
                .Index(t => t.DepartementID);
            
            CreateTable(
                "dbo.Departement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        CP = c.String(),
                        Region_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Region", t => t.Region_ID)
                .Index(t => t.Region_ID);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Moto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Modele = c.String(nullable: false),
                        MarqueID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                        Cylindree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genre", t => t.GenreID)
                .ForeignKey("dbo.Marque", t => t.MarqueID)
                .Index(t => t.MarqueID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Marque",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Taille = c.Int(nullable: false),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        AnnonceID = c.Int(nullable: false),
                        Principale = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Annonce", t => t.AnnonceID, cascadeDelete: true)
                .Index(t => t.AnnonceID);
            
            CreateTable(
                "dbo.AnnonceGenre",
                c => new
                    {
                        Annonce_ID = c.Int(nullable: false),
                        Genre_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Annonce_ID, t.Genre_ID })
                .ForeignKey("dbo.Annonce", t => t.Annonce_ID, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_ID, cascadeDelete: true)
                .Index(t => t.Annonce_ID)
                .Index(t => t.Genre_ID);
            
            CreateTable(
                "dbo.AnnonceMarque",
                c => new
                    {
                        Annonce_ID = c.Int(nullable: false),
                        Marque_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Annonce_ID, t.Marque_ID })
                .ForeignKey("dbo.Annonce", t => t.Annonce_ID, cascadeDelete: true)
                .ForeignKey("dbo.Marque", t => t.Marque_ID, cascadeDelete: true)
                .Index(t => t.Annonce_ID)
                .Index(t => t.Marque_ID);
            
            CreateTable(
                "dbo.AnnonceMoto",
                c => new
                    {
                        Annonce_ID = c.Int(nullable: false),
                        Moto_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Annonce_ID, t.Moto_ID })
                .ForeignKey("dbo.Annonce", t => t.Annonce_ID, cascadeDelete: true)
                .ForeignKey("dbo.Moto", t => t.Moto_ID, cascadeDelete: true)
                .Index(t => t.Annonce_ID)
                .Index(t => t.Moto_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "AnnonceID", "dbo.Annonce");
            DropForeignKey("dbo.AnnonceMoto", "Moto_ID", "dbo.Moto");
            DropForeignKey("dbo.AnnonceMoto", "Annonce_ID", "dbo.Annonce");
            DropForeignKey("dbo.Annonce", "MotoProposeeID", "dbo.Moto");
            DropForeignKey("dbo.AnnonceMarque", "Marque_ID", "dbo.Marque");
            DropForeignKey("dbo.AnnonceMarque", "Annonce_ID", "dbo.Annonce");
            DropForeignKey("dbo.AnnonceGenre", "Genre_ID", "dbo.Genre");
            DropForeignKey("dbo.AnnonceGenre", "Annonce_ID", "dbo.Annonce");
            DropForeignKey("dbo.Moto", "MarqueID", "dbo.Marque");
            DropForeignKey("dbo.Moto", "GenreID", "dbo.Genre");
            DropForeignKey("dbo.Annonce", "DepartementID", "dbo.Departement");
            DropForeignKey("dbo.Departement", "Region_ID", "dbo.Region");
            DropIndex("dbo.AnnonceMoto", new[] { "Moto_ID" });
            DropIndex("dbo.AnnonceMoto", new[] { "Annonce_ID" });
            DropIndex("dbo.AnnonceMarque", new[] { "Marque_ID" });
            DropIndex("dbo.AnnonceMarque", new[] { "Annonce_ID" });
            DropIndex("dbo.AnnonceGenre", new[] { "Genre_ID" });
            DropIndex("dbo.AnnonceGenre", new[] { "Annonce_ID" });
            DropIndex("dbo.Photo", new[] { "AnnonceID" });
            DropIndex("dbo.Moto", new[] { "GenreID" });
            DropIndex("dbo.Moto", new[] { "MarqueID" });
            DropIndex("dbo.Departement", new[] { "Region_ID" });
            DropIndex("dbo.Annonce", new[] { "DepartementID" });
            DropIndex("dbo.Annonce", new[] { "MotoProposeeID" });
            DropTable("dbo.AnnonceMoto");
            DropTable("dbo.AnnonceMarque");
            DropTable("dbo.AnnonceGenre");
            DropTable("dbo.Photo");
            DropTable("dbo.Marque");
            DropTable("dbo.Moto");
            DropTable("dbo.Genre");
            DropTable("dbo.Region");
            DropTable("dbo.Departement");
            DropTable("dbo.Annonce");
        }
    }
}
