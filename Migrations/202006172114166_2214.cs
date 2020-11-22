namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2214 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUserOrgs", "Org_OrgId", "dbo.Orgs");
            DropIndex("dbo.RegisteredUserOrgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropIndex("dbo.RegisteredUserOrgs", new[] { "Org_OrgId" });
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
            
            CreateIndex("dbo.RegisteredUserOrgs", "Org_OrgId");
            CreateIndex("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.RegisteredUserOrgs", "Org_OrgId", "dbo.Orgs", "OrgId", cascadeDelete: true);
            AddForeignKey("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId", cascadeDelete: true);
        }
    }
}
