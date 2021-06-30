namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210706 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgSchCalendars", "Isarchived", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgSchCalendars", "Isarchived");
        }
    }
}
