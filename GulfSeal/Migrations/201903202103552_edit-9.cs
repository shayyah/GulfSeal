namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Languages", "FlagImageUrl", c => c.String(nullable: false));
            AddColumn("dbo.Languages", "FlagImageName", c => c.String());
            DropColumn("dbo.Languages", "Flag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "Flag", c => c.String(nullable: false));
            DropColumn("dbo.Languages", "FlagImageName");
            DropColumn("dbo.Languages", "FlagImageUrl");
        }
    }
}
