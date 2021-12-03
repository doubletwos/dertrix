namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210806 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Org_Events_Log", "Org_Events_Types", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Org_Events_Log", "Org_Events_Types");
        }
    }
}
