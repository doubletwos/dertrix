namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210807 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Org_Events_Log", "Org_Event_TypeId");
            DropColumn("dbo.Org_Events_Log", "Org_Event_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Org_Events_Log", "Org_Event_Name", c => c.String());
            AddColumn("dbo.Org_Events_Log", "Org_Event_TypeId", c => c.Int(nullable: false));
        }
    }
}
