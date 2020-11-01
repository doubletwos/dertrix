namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2007 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredUsersGroups",
                c => new
                    {
                        RegisteredUsersGroupsId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        OrgGroupId = c.Int(nullable: false),
                        GroupTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUsersGroupsId)
                .ForeignKey("dbo.OrgGroups", t => t.OrgGroupId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.OrgGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsersGroups", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUsersGroups", "OrgGroupId", "dbo.OrgGroups");
            DropIndex("dbo.RegisteredUsersGroups", new[] { "OrgGroupId" });
            DropIndex("dbo.RegisteredUsersGroups", new[] { "RegisteredUserId" });
            DropTable("dbo.RegisteredUsersGroups");
        }
    }
}
