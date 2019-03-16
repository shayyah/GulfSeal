namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Families", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Rank");
            DropColumn("dbo.Families", "Rank");
        }
    }
}
