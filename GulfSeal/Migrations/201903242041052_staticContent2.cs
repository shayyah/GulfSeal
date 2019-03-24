namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staticContent2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaticContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        About = c.String(),
                        Privacy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaticContentTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        About = c.String(),
                        Privacy = c.String(),
                        LanguageId = c.Int(nullable: false),
                        StaticContentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.StaticContents", t => t.StaticContentId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.StaticContentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaticContentTranslations", "StaticContentId", "dbo.StaticContents");
            DropForeignKey("dbo.StaticContentTranslations", "LanguageId", "dbo.Languages");
            DropIndex("dbo.StaticContentTranslations", new[] { "StaticContentId" });
            DropIndex("dbo.StaticContentTranslations", new[] { "LanguageId" });
            DropTable("dbo.StaticContentTranslations");
            DropTable("dbo.StaticContents");
        }
    }
}
