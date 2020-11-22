namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0655 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orgs", "OrgName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String(maxLength: 30));
            AlterColumn("dbo.Orgs", "OrgName", c => c.String(maxLength: 30));
        }
    }
}
