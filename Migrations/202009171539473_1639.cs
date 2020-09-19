namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1639 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "IsTester", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegisteredUsers", "IsTesterOrgId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "IsTesterOrgId");
            DropColumn("dbo.RegisteredUsers", "IsTester");
        }
    }
}
