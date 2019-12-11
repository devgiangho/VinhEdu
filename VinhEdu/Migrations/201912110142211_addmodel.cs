namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Class", "Grade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Class", "Grade");
        }
    }
}
