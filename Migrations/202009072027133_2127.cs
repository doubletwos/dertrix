namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2127 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgBrands", "OrgNavBarTextColour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgBrands", "OrgNavBarTextColour");
        }
    }
}
