namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1326 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgBrands", "OrgBrandBar", c => c.String());
            AddColumn("dbo.OrgBrands", "OrgNavigationBar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgBrands", "OrgNavigationBar");
            DropColumn("dbo.OrgBrands", "OrgBrandBar");
        }
    }
}
