namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2356 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telephone = c.String(),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUserId)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        UserTypeName = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            AddColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId", c => c.Int());
            CreateIndex("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.RegisteredUsers", new[] { "UserTypeId" });
            DropIndex("dbo.Orgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            DropTable("dbo.UserTypes");
            DropTable("dbo.RegisteredUsers");
        }
    }
}
