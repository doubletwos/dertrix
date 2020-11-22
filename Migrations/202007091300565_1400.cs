namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1400 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            AddColumn("dbo.OrgTypes", "OrgId", c => c.Int());
            CreateIndex("dbo.OrgTypes", "OrgId");
            AddForeignKey("dbo.OrgTypes", "OrgId", "dbo.Orgs", "OrgId");
            DropColumn("dbo.Orgs", "OrgTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orgs", "OrgTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrgTypes", "OrgId", "dbo.Orgs");
            DropIndex("dbo.OrgTypes", new[] { "OrgId" });
            DropColumn("dbo.OrgTypes", "OrgId");
            CreateIndex("dbo.Orgs", "OrgTypeId");
            AddForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes", "OrgTypeId", cascadeDelete: true);
        }
    }
}
