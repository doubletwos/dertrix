namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2114 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredUserOrganisations",
                c => new
                    {
                        RegisteredUserOrganisationId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        OrgId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUserOrganisationId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.OrgId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUserOrganisations", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUserOrganisations", "OrgId", "dbo.Orgs");
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "OrgId" });
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "RegisteredUserId" });
            DropTable("dbo.RegisteredUserOrganisations");
        }
    }
}
