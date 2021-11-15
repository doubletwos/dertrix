namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210801 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgGroups", "Group_members_count", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgGroups", "Group_members_count");
        }
    }
}
