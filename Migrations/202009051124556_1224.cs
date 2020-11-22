namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1224 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUserOrganisations", "RegUserOrgBrand", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUserOrganisations", "RegUserOrgBrand", c => c.String());
        }
    }
}
