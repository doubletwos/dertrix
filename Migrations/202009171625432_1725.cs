namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1725 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RegisteredUsers", "IsTester");
            DropColumn("dbo.RegisteredUsers", "IsTesterOrgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsers", "IsTesterOrgId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "IsTester", c => c.Boolean(nullable: false));
        }
    }
}
