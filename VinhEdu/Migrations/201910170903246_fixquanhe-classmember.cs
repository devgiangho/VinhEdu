namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixquanheclassmember : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Attendance", new[] { "ClassID" });
            RenameColumn(table: "dbo.Attendance", name: "ClassID", newName: "Class_ClassID");
            AddColumn("dbo.ClassMember", "LearnStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Attendance", "Class_ClassID", c => c.Int());
            CreateIndex("dbo.Attendance", "Class_ClassID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Attendance", new[] { "Class_ClassID" });
            AlterColumn("dbo.Attendance", "Class_ClassID", c => c.Int(nullable: false));
            DropColumn("dbo.ClassMember", "LearnStatus");
            RenameColumn(table: "dbo.Attendance", name: "Class_ClassID", newName: "ClassID");
            CreateIndex("dbo.Attendance", "ClassID");
        }
    }
}
