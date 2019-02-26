namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproduct2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FamilyTransitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        FamilyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Families", t => t.FamilyId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.FamilyId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Thickness = c.String(),
                        Length = c.String(),
                        Width = c.String(),
                        ReinforcedType = c.String(),
                        ReinforcedDensity = c.String(),
                        ServiceType = c.String(),
                        FileName = c.String(),
                        Link = c.String(),
                        Extinstion = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                        FamilyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Families", t => t.FamilyId, cascadeDelete: true)
                .Index(t => t.FamilyId);
            
            CreateTable(
                "dbo.ProductTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Thickness = c.String(),
                        Length = c.String(),
                        Width = c.String(),
                        ReinforcedType = c.String(),
                        ReinforcedDensity = c.String(),
                        ServiceType = c.String(),
                        LanguageId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTranslations", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductTranslations", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Products", "FamilyId", "dbo.Families");
            DropForeignKey("dbo.FamilyTransitions", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.FamilyTransitions", "FamilyId", "dbo.Families");
            DropIndex("dbo.ProductTranslations", new[] { "ProductId" });
            DropIndex("dbo.ProductTranslations", new[] { "LanguageId" });
            DropIndex("dbo.Products", new[] { "FamilyId" });
            DropIndex("dbo.FamilyTransitions", new[] { "FamilyId" });
            DropIndex("dbo.FamilyTransitions", new[] { "LanguageId" });
            DropTable("dbo.ProductTranslations");
            DropTable("dbo.Products");
            DropTable("dbo.FamilyTransitions");
            DropTable("dbo.Families");
        }
    }
}
