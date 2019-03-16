namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.partenrs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogoLink = c.String(nullable: false),
                        name = c.String(nullable: false),
                        order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.partenrs");
        }
    }
}
