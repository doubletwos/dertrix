namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "IsTester", c => c.Boolean());
            AddColumn("dbo.RegisteredUserOrganisations", "RegisteredUserTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUserOrganisations", "RegisteredUserTypeId");
            DropColumn("dbo.RegisteredUserOrganisations", "IsTester");
        }
    }
}
