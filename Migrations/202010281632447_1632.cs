namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1632 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgGroups", "GroupName", c => c.String());
            AddColumn("dbo.OrgGroups", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrgGroups", "CreationDate");
            DropColumn("dbo.OrgGroups", "GroupName");
        }
    }
}
