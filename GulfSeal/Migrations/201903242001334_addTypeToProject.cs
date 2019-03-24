namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTypeToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ProjectType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "ProjectType");
        }
    }
}
