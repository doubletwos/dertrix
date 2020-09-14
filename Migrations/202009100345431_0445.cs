namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0445 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgBrands", "OrgBrandButtonColour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgBrands", "OrgBrandButtonColour");
        }
    }
}
