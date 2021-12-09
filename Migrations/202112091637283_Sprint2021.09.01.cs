namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210901 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "RegistrationFlags", c => c.Int(nullable: false));
            DropColumn("dbo.RegisteredUserOrganisations", "AddedVia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "AddedVia", c => c.Int());
            DropColumn("dbo.RegisteredUserOrganisations", "RegistrationFlags");
        }
    }
}
