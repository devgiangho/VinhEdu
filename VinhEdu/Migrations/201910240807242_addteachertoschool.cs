namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addteachertoschool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "SchoolID", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "SchoolID");
            AddForeignKey("dbo.User", "SchoolID", "dbo.School", "SchoolID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "SchoolID", "dbo.School");
            DropIndex("dbo.User", new[] { "SchoolID" });
            DropColumn("dbo.User", "SchoolID");
        }
    }
}
