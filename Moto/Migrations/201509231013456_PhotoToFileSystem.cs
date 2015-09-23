namespace Motonet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoToFileSystem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "CheminComplet", c => c.String());
            DropColumn("dbo.Photo", "ContentType");
            DropColumn("dbo.Photo", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photo", "Content", c => c.Binary());
            AddColumn("dbo.Photo", "ContentType", c => c.String(maxLength: 100));
            DropColumn("dbo.Photo", "CheminComplet");
        }
    }
}
