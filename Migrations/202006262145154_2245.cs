namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2245 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "FirstName", c => c.String());
            AddColumn("dbo.RegisteredUserOrganisations", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "LastName");
            DropColumn("dbo.RegisteredUserOrganisations", "FirstName");
        }
    }
}
