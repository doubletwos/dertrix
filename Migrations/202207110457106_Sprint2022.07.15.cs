namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220715 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgClassPeriods", "Updater_Id", c => c.Int());
            AddColumn("dbo.OrgClassPeriods", "Last_updated_date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgClassPeriods", "Last_updated_date");
            DropColumn("dbo.OrgClassPeriods", "Updater_Id");
        }
    }
}
