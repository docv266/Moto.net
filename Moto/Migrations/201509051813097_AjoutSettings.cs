namespace Motonet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdminPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
