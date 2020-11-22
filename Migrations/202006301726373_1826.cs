namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1826 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "Org_OrgId", "dbo.Orgs");
            DropIndex("dbo.Classes", new[] { "Org_OrgId" });
            RenameColumn(table: "dbo.Classes", name: "Org_OrgId", newName: "OrgId");
            AlterColumn("dbo.Classes", "OrgId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "OrgId");
            AddForeignKey("dbo.Classes", "OrgId", "dbo.Orgs", "OrgId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "OrgId", "dbo.Orgs");
            DropIndex("dbo.Classes", new[] { "OrgId" });
            AlterColumn("dbo.Classes", "OrgId", c => c.Int());
            RenameColumn(table: "dbo.Classes", name: "OrgId", newName: "Org_OrgId");
            CreateIndex("dbo.Classes", "Org_OrgId");
            AddForeignKey("dbo.Classes", "Org_OrgId", "dbo.Orgs", "OrgId");
        }
    }
}
