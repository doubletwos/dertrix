namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220505 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "OtherNames", c => c.String());
            AddColumn("dbo.RegisteredUserOrganisations", "OtherNames", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "OtherNames");
            DropColumn("dbo.RegisteredUsers", "OtherNames");
        }
    }
}
