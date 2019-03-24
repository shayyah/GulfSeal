namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        ImageURL = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTranslations", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectTranslations", "LanguageId", "dbo.Languages");
            DropIndex("dbo.ProjectTranslations", new[] { "ProjectId" });
            DropIndex("dbo.ProjectTranslations", new[] { "LanguageId" });
            DropTable("dbo.ProjectTranslations");
            DropTable("dbo.Projects");
        }
    }
}
