namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220501 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students_Grades_Log", "RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Students_Grades_Log", new[] { "RegisteredUserId" });
            DropColumn("dbo.Students_Grades_Log", "RegisteredUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students_Grades_Log", "RegisteredUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students_Grades_Log", "RegisteredUserId");
            AddForeignKey("dbo.Students_Grades_Log", "RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId", cascadeDelete: false);
        }
    }
}
