namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1716 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "GroupTypeId", "dbo.GroupTypes");
            DropForeignKey("dbo.Orgs", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.OrgGroups", "GroupId", "dbo.Groups");
            DropIndex("dbo.Orgs", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "GroupTypeId" });
            DropIndex("dbo.OrgGroups", new[] { "GroupId" });
            AddColumn("dbo.OrgGroups", "GroupTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.OrgGroups", "GroupOrgTypeId", c => c.Int());
            AddColumn("dbo.OrgGroups", "GroupRefNumb", c => c.Int());
            CreateIndex("dbo.OrgGroups", "GroupTypeId");
            AddForeignKey("dbo.OrgGroups", "GroupTypeId", "dbo.GroupTypes", "GroupTypeId", cascadeDelete: true);
            DropColumn("dbo.Orgs", "GroupId");
            DropColumn("dbo.OrgGroups", "GroupId");
            DropTable("dbo.Groups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        GroupTypeId = c.Int(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            AddColumn("dbo.OrgGroups", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.Orgs", "GroupId", c => c.Int());
            DropForeignKey("dbo.OrgGroups", "GroupTypeId", "dbo.GroupTypes");
            DropIndex("dbo.OrgGroups", new[] { "GroupTypeId" });
            DropColumn("dbo.OrgGroups", "GroupRefNumb");
            DropColumn("dbo.OrgGroups", "GroupOrgTypeId");
            DropColumn("dbo.OrgGroups", "GroupTypeId");
            CreateIndex("dbo.OrgGroups", "GroupId");
            CreateIndex("dbo.Groups", "GroupTypeId");
            CreateIndex("dbo.Orgs", "GroupId");
            AddForeignKey("dbo.OrgGroups", "GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.Orgs", "GroupId", "dbo.Groups", "GroupId");
            AddForeignKey("dbo.Groups", "GroupTypeId", "dbo.GroupTypes", "GroupTypeId");
        }
    }
}
