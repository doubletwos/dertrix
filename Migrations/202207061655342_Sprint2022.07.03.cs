namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220703 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgSchDays", "DayTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgSchDays", "DayTypeId");
        }
    }
}
