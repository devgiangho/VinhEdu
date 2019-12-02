namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointBoard", "SubjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.PointBoard", "SubjectID");
            AddForeignKey("dbo.PointBoard", "SubjectID", "dbo.Subject", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PointBoard", "SubjectID", "dbo.Subject");
            DropIndex("dbo.PointBoard", new[] { "SubjectID" });
            DropColumn("dbo.PointBoard", "SubjectID");
        }
    }
}
