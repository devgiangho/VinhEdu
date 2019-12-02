namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editpointboard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointBoard", "ConfigureID", c => c.Int(nullable: false));
            CreateIndex("dbo.PointBoard", "ConfigureID");
            AddForeignKey("dbo.PointBoard", "ConfigureID", "dbo.Configures", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PointBoard", "ConfigureID", "dbo.Configures");
            DropIndex("dbo.PointBoard", new[] { "ConfigureID" });
            DropColumn("dbo.PointBoard", "ConfigureID");
        }
    }
}
