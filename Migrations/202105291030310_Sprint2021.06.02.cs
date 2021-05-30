namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210602 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Org_Events_Log", "OrgId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Org_Events_Log", "OrgId");
        }
    }
}
