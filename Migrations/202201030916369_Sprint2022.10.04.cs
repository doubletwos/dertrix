namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221004 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrgBrands", "OrgBrandBar");
            DropColumn("dbo.OrgBrands", "OrgNavigationBar");
            DropColumn("dbo.OrgBrands", "OrgNavBarTextColour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgBrands", "OrgNavBarTextColour", c => c.String());
            AddColumn("dbo.OrgBrands", "OrgNavigationBar", c => c.String());
            AddColumn("dbo.OrgBrands", "OrgBrandBar", c => c.String());
        }
    }
}
