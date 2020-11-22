namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgOrgTypes", "OrgName", c => c.String());
            AddColumn("dbo.OrgOrgTypes", "OrgTypeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgOrgTypes", "OrgTypeName");
            DropColumn("dbo.OrgOrgTypes", "OrgName");
        }
    }
}
