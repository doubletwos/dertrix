namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "NurserySchoolUserRoleId", c => c.Int());
            CreateIndex("dbo.RegisteredUserOrganisations", "NurserySchoolUserRoleId");
            AddForeignKey("dbo.RegisteredUserOrganisations", "NurserySchoolUserRoleId", "dbo.NurserySchoolUserRoles", "NurserySchoolUserRoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUserOrganisations", "NurserySchoolUserRoleId", "dbo.NurserySchoolUserRoles");
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "NurserySchoolUserRoleId" });
            DropColumn("dbo.RegisteredUserOrganisations", "NurserySchoolUserRoleId");
        }
    }
}
