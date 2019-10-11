namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "StudentID", c => c.String(maxLength: 50, unicode: false));
            CreateIndex("dbo.User", "StudentID", unique: true);
            CreateIndex("dbo.User", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.User", new[] { "StudentID" });
            AlterColumn("dbo.User", "StudentID", c => c.String(maxLength: 20, unicode: false));
        }
    }
}
