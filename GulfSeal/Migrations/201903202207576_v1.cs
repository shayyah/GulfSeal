namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
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
            
            AddColumn("dbo.Languages", "FlagImageUrl", c => c.String(nullable: false));
            AddColumn("dbo.Languages", "FlagImageName", c => c.String());
            DropColumn("dbo.Languages", "Flag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "Flag", c => c.String(nullable: false));
            DropForeignKey("dbo.InformationTranslations", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.InformationTranslations", "InformationId", "dbo.Information");
            DropIndex("dbo.InformationTranslations", new[] { "InformationId" });
            DropIndex("dbo.InformationTranslations", new[] { "LanguageId" });
            DropColumn("dbo.Languages", "FlagImageName");
            DropColumn("dbo.Languages", "FlagImageUrl");
            DropTable("dbo.InformationTranslations");
        }
    }
}
