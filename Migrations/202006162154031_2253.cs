namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2253 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "OrgId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "OrgId");
        }
    }
}
