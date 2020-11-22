namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2228 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Orgs", new[] { "RegisteredUser_RegisteredUserId" });
            CreateTable(
                "dbo.RegisteredUserOrgs",
                c => new
                    {
                        RegisteredUser_RegisteredUserId = c.Int(nullable: false),
                        Org_OrgId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RegisteredUser_RegisteredUserId, t.Org_OrgId })
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUser_RegisteredUserId, cascadeDelete: true)
                .ForeignKey("dbo.Orgs", t => t.Org_OrgId, cascadeDelete: true)
                .Index(t => t.RegisteredUser_RegisteredUserId)
                .Index(t => t.Org_OrgId);
            
            DropColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId", c => c.Int());
            DropForeignKey("dbo.RegisteredUserOrgs", "Org_OrgId", "dbo.Orgs");
            DropForeignKey("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.RegisteredUserOrgs", new[] { "Org_OrgId" });
            DropIndex("dbo.RegisteredUserOrgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropTable("dbo.RegisteredUserOrgs");
            CreateIndex("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
    }
}
