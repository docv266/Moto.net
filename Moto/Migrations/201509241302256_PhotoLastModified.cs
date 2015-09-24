namespace Motonet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoLastModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "ModifiedDate");
        }
    }
}
