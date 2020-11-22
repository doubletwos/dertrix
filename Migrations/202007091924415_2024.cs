namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2024 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrgOrgTypes", "OrgTypeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgOrgTypes", "OrgTypeName", c => c.String());
        }
    }
}
