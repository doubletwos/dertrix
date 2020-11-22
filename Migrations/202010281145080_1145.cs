namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1145 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupTypes", "GroupOrgTypeId", c => c.Int());
            AddColumn("dbo.GroupTypes", "GroupRefNumb", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupTypes", "GroupRefNumb");
            DropColumn("dbo.GroupTypes", "GroupOrgTypeId");
        }
    }
}
