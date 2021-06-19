namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210704 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgSchCalendars", "Description", c => c.String());
            AddColumn("dbo.OrgSchCalendars", "EventTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgSchCalendars", "EventTime");
            DropColumn("dbo.OrgSchCalendars", "Description");
        }
    }
}
