namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec202011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "PrimarySchoolUserRoleId", c => c.Int());
            AddColumn("dbo.RegisteredUserOrganisations", "SecondarySchoolUserRoleId", c => c.Int());
            AddColumn("dbo.RegisteredUserOrganisations", "EnrolmentDate", c => c.DateTime());
            AddColumn("dbo.RegisteredUserOrganisations", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "CreatedBy");
            DropColumn("dbo.RegisteredUserOrganisations", "EnrolmentDate");
            DropColumn("dbo.RegisteredUserOrganisations", "SecondarySchoolUserRoleId");
            DropColumn("dbo.RegisteredUserOrganisations", "PrimarySchoolUserRoleId");
        }
    }
}
