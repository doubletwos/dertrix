namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20204 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrgGroups", "IsSelected", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrgGroups", "IsSelected", c => c.Boolean());
        }
    }
}
