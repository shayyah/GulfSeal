namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        InformationURL = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                        InformationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InformationTypes", t => t.InformationTypeId, cascadeDelete: true)
                .Index(t => t.InformationTypeId);
            
            CreateTable(
                "dbo.InformationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Extentions = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Information", "InformationTypeId", "dbo.InformationTypes");
            DropIndex("dbo.Information", new[] { "InformationTypeId" });
            DropTable("dbo.InformationTypes");
            DropTable("dbo.Information");
        }
    }
}
