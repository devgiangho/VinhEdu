namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editsemmester : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "Semester", c => c.Int(nullable: false));
            DropColumn("dbo.Settings", "Semmester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "Semmester", c => c.Int(nullable: false));
            DropColumn("dbo.Settings", "Semester");
        }
    }
}
