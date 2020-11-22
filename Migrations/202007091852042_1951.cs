namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1951 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgOrgTypes",
                c => new
                    {
                        OrgOrgTypeId = c.Int(nullable: false, identity: true),
                        OrgId = c.Int(nullable: false),
                        OrgTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgOrgTypeId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .ForeignKey("dbo.OrgTypes", t => t.OrgTypeId, cascadeDelete: true)
                .Index(t => t.OrgId)
                .Index(t => t.OrgTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrgOrgTypes", "OrgTypeId", "dbo.OrgTypes");
            DropForeignKey("dbo.OrgOrgTypes", "OrgId", "dbo.Orgs");
            DropIndex("dbo.OrgOrgTypes", new[] { "OrgTypeId" });
            DropIndex("dbo.OrgOrgTypes", new[] { "OrgId" });
            DropTable("dbo.OrgOrgTypes");
        }
    }
}
