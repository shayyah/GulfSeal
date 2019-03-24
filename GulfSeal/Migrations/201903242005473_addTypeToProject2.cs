namespace GulfSeal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTypeToProject2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "ProjectType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "ProjectType", c => c.Int(nullable: false));
        }
    }
}
