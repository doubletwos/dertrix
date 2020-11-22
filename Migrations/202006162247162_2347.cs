namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2347 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUserOrgs", "Org_OrgId", "dbo.Orgs");
            DropIndex("dbo.RegisteredUserOrgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropIndex("dbo.RegisteredUserOrgs", new[] { "Org_OrgId" });
            AddColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "OrgId", c => c.Int(nullable: false));
            AddColumn("dbo.RegisteredUsers", "Org_OrgId", c => c.Int());
            CreateIndex("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            CreateIndex("dbo.RegisteredUsers", "OrgId");
            CreateIndex("dbo.RegisteredUsers", "Org_OrgId");
            AddForeignKey("dbo.RegisteredUsers", "OrgId", "dbo.Orgs", "OrgId", cascadeDelete: true);
            AddForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
            AddForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs", "OrgId");
            DropTable("dbo.RegisteredUserOrgs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RegisteredUserOrgs",
                c => new
                    {
                        RegisteredUser_RegisteredUserId = c.Int(nullable: false),
                        Org_OrgId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RegisteredUser_RegisteredUserId, t.Org_OrgId });
            
            DropForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs");
            DropForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUsers", "OrgId", "dbo.Orgs");
            DropIndex("dbo.RegisteredUsers", new[] { "Org_OrgId" });
            DropIndex("dbo.RegisteredUsers", new[] { "OrgId" });
            DropIndex("dbo.Orgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropColumn("dbo.RegisteredUsers", "Org_OrgId");
            DropColumn("dbo.RegisteredUsers", "OrgId");
            DropColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            CreateIndex("dbo.RegisteredUserOrgs", "Org_OrgId");
            CreateIndex("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.RegisteredUserOrgs", "Org_OrgId", "dbo.Orgs", "OrgId", cascadeDelete: true);
            AddForeignKey("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId", cascadeDelete: true);
        }
    }
}
