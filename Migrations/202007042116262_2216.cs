namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2216 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredUserRoles",
                c => new
                    {
                        RegisteredUserRoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RegisteredUserRoleID);
            
            AddColumn("dbo.RegisteredUsers", "RegisteredUserRoleID", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "RegisteredUserRoleID");
            AddForeignKey("dbo.RegisteredUsers", "RegisteredUserRoleID", "dbo.RegisteredUserRoles", "RegisteredUserRoleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserRoleID", "dbo.RegisteredUserRoles");
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserRoleID" });
            DropColumn("dbo.RegisteredUsers", "RegisteredUserRoleID");
            DropTable("dbo.RegisteredUserRoles");
        }
    }
}
