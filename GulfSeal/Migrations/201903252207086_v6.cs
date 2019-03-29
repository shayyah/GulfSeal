namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
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
                        page = c.String(),
                        image_url = c.String(),
                        sim_key = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.langId, cascadeDelete: true)
                .Index(t => t.langId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sec_lang", "langId", "dbo.Languages");
            DropIndex("dbo.sec_lang", new[] { "langId" });
            DropTable("dbo.sec_lang");
        }
    }
}
