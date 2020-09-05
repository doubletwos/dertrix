namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2119 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "OrgBrandBar");
            DropColumn("dbo.RegisteredUserOrganisations", "OrgNavigationBar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "OrgNavigationBar", c => c.String());
            AddColumn("dbo.RegisteredUserOrganisations", "OrgBrandBar", c => c.String());
        }
    }
}
