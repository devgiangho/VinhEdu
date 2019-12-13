namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfinished : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "isFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "isFinished");
        }
    }
}
