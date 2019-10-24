namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addteachertoschool1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "SchoolID" });
            AlterColumn("dbo.User", "SchoolID", c => c.Int());
            CreateIndex("dbo.User", "SchoolID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "SchoolID" });
            AlterColumn("dbo.User", "SchoolID", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "SchoolID");
        }
    }
}
