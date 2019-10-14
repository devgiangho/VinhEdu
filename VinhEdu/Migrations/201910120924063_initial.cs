namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                        Status = c.Int(),
                        ConfigureID = c.Int(nullable: false),
                        Semmester = c.Int(nullable: false),
                        AttendanceDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Class", t => t.ClassID)
                .ForeignKey("dbo.Configures", t => t.ConfigureID)
                .ForeignKey("dbo.User", t => t.StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.ClassID)
                .Index(t => t.ConfigureID);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 100),
                        SchoolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.School", t => t.SchoolID)
                .Index(t => t.SchoolID);
            
            CreateTable(
                "dbo.ClassMember",
                c => new
                    {
                        ClassID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ConfigureID = c.Int(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        IsHomeTeacher = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.ClassID, t.UserID, t.ConfigureID })
                .ForeignKey("dbo.Configures", t => t.ConfigureID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.Class", t => t.ClassID)
                .Index(t => t.ClassID)
                .Index(t => t.UserID)
                .Index(t => t.ConfigureID);
            
            CreateTable(
                "dbo.Configures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolYear = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Identifier = c.String(maxLength: 100, unicode: false),
                        SubjectID = c.Int(),
                        Role = c.String(nullable: false, maxLength: 15, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500),
                        Type = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Subject", t => t.SubjectID)
                .Index(t => t.Identifier, unique: true)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.PointBoard",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                        ScoreX1 = c.String(maxLength: 500),
                        ScoreX2 = c.String(maxLength: 500),
                        ScoreX3 = c.String(maxLength: 200),
                        Semester = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.StudentID)
                .ForeignKey("dbo.Class", t => t.ClassID)
                .Index(t => t.StudentID)
                .Index(t => t.ClassID);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        SchoolID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(nullable: false, maxLength: 100),
                        HeadMasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolID);
            
            CreateTable(
                "dbo.BaseClassLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Class", "SchoolID", "dbo.School");
            DropForeignKey("dbo.PointBoard", "ClassID", "dbo.Class");
            DropForeignKey("dbo.ClassMember", "ClassID", "dbo.Class");
            DropForeignKey("dbo.User", "SubjectID", "dbo.Subject");
            DropForeignKey("dbo.PointBoard", "StudentID", "dbo.User");
            DropForeignKey("dbo.ClassMember", "UserID", "dbo.User");
            DropForeignKey("dbo.Attendance", "StudentID", "dbo.User");
            DropForeignKey("dbo.ClassMember", "ConfigureID", "dbo.Configures");
            DropForeignKey("dbo.Attendance", "ConfigureID", "dbo.Configures");
            DropForeignKey("dbo.Attendance", "ClassID", "dbo.Class");
            DropIndex("dbo.PointBoard", new[] { "ClassID" });
            DropIndex("dbo.PointBoard", new[] { "StudentID" });
            DropIndex("dbo.User", new[] { "SubjectID" });
            DropIndex("dbo.User", new[] { "Identifier" });
            DropIndex("dbo.ClassMember", new[] { "ConfigureID" });
            DropIndex("dbo.ClassMember", new[] { "UserID" });
            DropIndex("dbo.ClassMember", new[] { "ClassID" });
            DropIndex("dbo.Class", new[] { "SchoolID" });
            DropIndex("dbo.Attendance", new[] { "ConfigureID" });
            DropIndex("dbo.Attendance", new[] { "ClassID" });
            DropIndex("dbo.Attendance", new[] { "StudentID" });
            DropTable("dbo.BaseClassLists");
            DropTable("dbo.School");
            DropTable("dbo.Subject");
            DropTable("dbo.PointBoard");
            DropTable("dbo.User");
            DropTable("dbo.Configures");
            DropTable("dbo.ClassMember");
            DropTable("dbo.Class");
            DropTable("dbo.Attendance");
        }
    }
}
