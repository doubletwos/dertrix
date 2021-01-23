namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20213 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "Title");
        }
    }
}
