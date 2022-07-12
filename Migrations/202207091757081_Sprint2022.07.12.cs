namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220712 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgClassPeriods", "OrgSchDayId", c => c.Int());
            AddColumn("dbo.OrgClassPeriods", "Day", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgClassPeriods", "Day");
            DropColumn("dbo.OrgClassPeriods", "OrgSchDayId");
        }
    }
}
