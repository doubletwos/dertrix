namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1830 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "OrgName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "OrgName");
        }
    }
}
