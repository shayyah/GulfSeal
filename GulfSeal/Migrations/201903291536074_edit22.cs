namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sec_lang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        body = c.String(),
                        langId = c.Int(nullable: false),
                        image_url = c.String(),
                        sim_key = c.String(),
                        page = c.String(),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.langId, cascadeDelete: true)
                .Index(t => t.langId);
            
            CreateTable(
                "dbo.slider_packeg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        slider_name = c.String(),
                        sublider_name = c.String(),
                        lang_id = c.Int(nullable: false),
                        lang_name = c.String(),
                        image_url = c.String(),
                        semilarity_key = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sec_lang", "langId", "dbo.Languages");
            DropIndex("dbo.sec_lang", new[] { "langId" });
            DropTable("dbo.slider_packeg");
            DropTable("dbo.sec_lang");
        }
    }
}
