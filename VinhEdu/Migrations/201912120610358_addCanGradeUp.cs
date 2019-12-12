namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCanGradeUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "canGradeUp", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "canGradeUp");
        }
    }
}
