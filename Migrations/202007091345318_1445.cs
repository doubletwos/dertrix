namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1445 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrgTypes", "OrgId", "dbo.Orgs");
            DropIndex("dbo.OrgTypes", new[] { "OrgId" });
            DropColumn("dbo.OrgTypes", "OrgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgTypes", "OrgId", c => c.Int());
            CreateIndex("dbo.OrgTypes", "OrgId");
            AddForeignKey("dbo.OrgTypes", "OrgId", "dbo.Orgs", "OrgId");
        }
    }
}
