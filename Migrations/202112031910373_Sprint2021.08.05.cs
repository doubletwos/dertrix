namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210805 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Org_Events_Log", "Org_Events_Types");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Org_Events_Log", "Org_Events_Types", c => c.Int(nullable: false));
        }
    }
}
