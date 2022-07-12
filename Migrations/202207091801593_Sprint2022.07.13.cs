namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220713 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OrgClassPeriods", "OrgSchDayId");
            AddForeignKey("dbo.OrgClassPeriods", "OrgSchDayId", "dbo.OrgSchDays", "OrgSchDayId");
            DropColumn("dbo.OrgClassPeriods", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgClassPeriods", "Day", c => c.String());
            DropForeignKey("dbo.OrgClassPeriods", "OrgSchDayId", "dbo.OrgSchDays");
            DropIndex("dbo.OrgClassPeriods", new[] { "OrgSchDayId" });
        }
    }
}
