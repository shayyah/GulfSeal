namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sec_lang", "type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.sec_lang", "type");
        }
    }
}
