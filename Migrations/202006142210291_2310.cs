namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2310 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrgBrands", "OrgBrandName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrgBrands", "OrgBrandName", c => c.Int(nullable: false));
        }
    }
}
