namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "NurserySchoolUserRoleId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "NurserySchoolUserRoleId");
            AddForeignKey("dbo.RegisteredUsers", "NurserySchoolUserRoleId", "dbo.NurserySchoolUserRoles", "NurserySchoolUserRoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "NurserySchoolUserRoleId", "dbo.NurserySchoolUserRoles");
            DropIndex("dbo.RegisteredUsers", new[] { "NurserySchoolUserRoleId" });
            DropColumn("dbo.RegisteredUsers", "NurserySchoolUserRoleId");
        }
    }
}
