namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.slider_packeg");
        }
    }
}
