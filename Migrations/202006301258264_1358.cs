namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1358 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Org_OrgId", c => c.Int());
            CreateIndex("dbo.Classes", "Org_OrgId");
            AddForeignKey("dbo.Classes", "Org_OrgId", "dbo.Orgs", "OrgId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "Org_OrgId", "dbo.Orgs");
            DropIndex("dbo.Classes", new[] { "Org_OrgId" });
            DropColumn("dbo.Classes", "Org_OrgId");
        }
    }
}
