namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0823 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            AlterColumn("dbo.Orgs", "OrgName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String(maxLength: 20));
            AlterColumn("dbo.Orgs", "OrgTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orgs", "OrgTypeId");
            AddForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes", "OrgTypeId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            AlterColumn("dbo.Orgs", "OrgTypeId", c => c.Int());
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String());
            AlterColumn("dbo.Orgs", "OrgName", c => c.String());
            CreateIndex("dbo.Orgs", "OrgTypeId");
            AddForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes", "OrgTypeId");
        }
    }
}
