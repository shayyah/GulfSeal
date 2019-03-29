namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.slider_packeg", "semilarity_key", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.slider_packeg", "semilarity_key", c => c.String());
        }
    }
}
