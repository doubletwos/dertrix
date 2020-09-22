namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0800 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orgs", "OrgName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String(maxLength: 20));
            AlterColumn("dbo.Orgs", "OrgName", c => c.String(maxLength: 20));
        }
    }
}
