namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1213 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUsers", "RegUserOrgBrand", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUsers", "RegUserOrgBrand", c => c.String());
        }
    }
}
