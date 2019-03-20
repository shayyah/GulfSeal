namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InformationTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        InformationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Information", t => t.InformationId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.InformationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InformationTranslations", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.InformationTranslations", "InformationId", "dbo.Information");
            DropIndex("dbo.InformationTranslations", new[] { "InformationId" });
            DropIndex("dbo.InformationTranslations", new[] { "LanguageId" });
            DropTable("dbo.InformationTranslations");
        }
    }
}
