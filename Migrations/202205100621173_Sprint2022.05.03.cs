namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220503 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students_Grades_Log", "RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Students_Grades_Log", new[] { "RegisteredUserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Students_Grades_Log", "RegisteredUserId");
            AddForeignKey("dbo.Students_Grades_Log", "RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId", cascadeDelete: true);
        }
    }
}
