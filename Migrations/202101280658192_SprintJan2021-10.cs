namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan202110 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RegisteredUserOrganisations", "PrimarySchoolUserRoleId");
            CreateIndex("dbo.RegisteredUserOrganisations", "SecondarySchoolUserRoleId");
            AddForeignKey("dbo.RegisteredUserOrganisations", "PrimarySchoolUserRoleId", "dbo.PrimarySchoolUserRoles", "PrimarySchoolUserRoleID");
            AddForeignKey("dbo.RegisteredUserOrganisations", "SecondarySchoolUserRoleId", "dbo.SecondarySchoolUserRoles", "SecondarySchoolUserRoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUserOrganisations", "SecondarySchoolUserRoleId", "dbo.SecondarySchoolUserRoles");
            DropForeignKey("dbo.RegisteredUserOrganisations", "PrimarySchoolUserRoleId", "dbo.PrimarySchoolUserRoles");
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "SecondarySchoolUserRoleId" });
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "PrimarySchoolUserRoleId" });
        }
    }
}
