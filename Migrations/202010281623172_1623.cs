namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1623 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgGroups",
                c => new
                    {
                        OrgGroupId = c.Int(nullable: false, identity: true),
                        OrgId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgGroupId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .Index(t => t.OrgId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrgGroups", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.OrgGroups", "GroupId", "dbo.Groups");
            DropIndex("dbo.OrgGroups", new[] { "GroupId" });
            DropIndex("dbo.OrgGroups", new[] { "OrgId" });
            DropTable("dbo.OrgGroups");
        }
    }
}
