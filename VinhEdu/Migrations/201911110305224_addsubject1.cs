namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsubject1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointBoard", "Score", c => c.String(maxLength: 1000));
            DropColumn("dbo.PointBoard", "ScoreX1");
            DropColumn("dbo.PointBoard", "ScoreX2");
            DropColumn("dbo.PointBoard", "ScoreX3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PointBoard", "ScoreX3", c => c.String(maxLength: 200));
            AddColumn("dbo.PointBoard", "ScoreX2", c => c.String(maxLength: 500));
            AddColumn("dbo.PointBoard", "ScoreX1", c => c.String(maxLength: 500));
            DropColumn("dbo.PointBoard", "Score");
        }
    }
}
