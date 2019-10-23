namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeisCurrent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClassMember", "IsCurrent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassMember", "IsCurrent", c => c.Boolean(nullable: false));
        }
    }
}
