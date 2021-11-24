namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210802 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "AddedVia", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "AddedVia");
        }
    }
}
