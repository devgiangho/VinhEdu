namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                        Message = c.String(),
                        ClassID = c.Int(nullable: false),
                        ConfigureID = c.Int(nullable: false),
                        SendFrom = c.Int(nullable: false),
                        SendTime = c.DateTime(nullable: false),
                        ReadTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Class", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Configures", t => t.ConfigureID, cascadeDelete: true)
                .Index(t => t.ClassID)
                .Index(t => t.ConfigureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "ConfigureID", "dbo.Configures");
            DropForeignKey("dbo.Contacts", "ClassID", "dbo.Class");
            DropIndex("dbo.Contacts", new[] { "ConfigureID" });
            DropIndex("dbo.Contacts", new[] { "ClassID" });
            DropTable("dbo.Contacts");
        }
    }
}
