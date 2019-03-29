namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.slider_packeg", "semilarity_key", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.slider_packeg", "semilarity_key");
        }
    }
}
