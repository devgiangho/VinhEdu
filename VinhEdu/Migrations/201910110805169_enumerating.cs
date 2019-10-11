namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumerating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseClassLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.User", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.User", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "CreateDate");
            DropColumn("dbo.User", "DateOfBirth");
            DropColumn("dbo.User", "Status");
            DropColumn("dbo.User", "Password");
            DropTable("dbo.BaseClassLists");
        }
    }
}
