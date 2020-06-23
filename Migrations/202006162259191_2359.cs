namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2359 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs");
            DropIndex("dbo.Orgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropIndex("dbo.RegisteredUsers", new[] { "OrgId" });
            DropIndex("dbo.RegisteredUsers", new[] { "Org_OrgId" });
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
            DropColumn("dbo.RegisteredUsers", "OrgId");
            DropColumn("dbo.RegisteredUsers", "Org_OrgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsers", "Org_OrgId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "OrgId", c => c.Int(nullable: false));
            AddColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId", c => c.Int());
            DropForeignKey("dbo.RegisteredUserOrgs", "Org_OrgId", "dbo.Orgs");
            DropForeignKey("dbo.RegisteredUserOrgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.RegisteredUserOrgs", new[] { "Org_OrgId" });
            DropIndex("dbo.RegisteredUserOrgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropTable("dbo.RegisteredUserOrgs");
            CreateIndex("dbo.RegisteredUsers", "Org_OrgId");
            CreateIndex("dbo.RegisteredUsers", "OrgId");
            CreateIndex("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs", "OrgId");
            AddForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
            AddForeignKey("dbo.RegisteredUsers", "OrgId", "dbo.Orgs", "OrgId", cascadeDelete: true);
        }
    }
}
