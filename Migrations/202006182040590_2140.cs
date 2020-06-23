namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2140 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "Org_OrgId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "Org_OrgId");
            AddForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs", "OrgId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs");
            DropIndex("dbo.RegisteredUsers", new[] { "Org_OrgId" });
            DropColumn("dbo.RegisteredUsers", "Org_OrgId");
        }
    }
}
