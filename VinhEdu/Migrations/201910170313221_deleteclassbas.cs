namespace VinhEdu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteclassbas : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.BaseClassLists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BaseClassLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
