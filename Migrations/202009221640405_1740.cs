namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1740 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            AlterColumn("dbo.Orgs", "OrgTypeId", c => c.Int());
            CreateIndex("dbo.Orgs", "OrgTypeId");
            AddForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes", "OrgTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            AlterColumn("dbo.Orgs", "OrgTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orgs", "OrgTypeId");
            AddForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes", "OrgTypeId", cascadeDelete: true);
        }
    }
}
