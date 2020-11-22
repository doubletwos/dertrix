namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2303 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RegisteredUsers", "OrgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsers", "OrgId", c => c.Int(nullable: false));
        }
    }
}
