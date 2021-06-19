namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210703 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgSchCalendars", "EventDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgSchCalendars", "EventDate");
        }
    }
}
