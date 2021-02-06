namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintFeb20211 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "LastLogOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "LastLogOn");
        }
    }
}
