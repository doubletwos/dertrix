namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1946 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.RegisteredUsers", new[] { "UserTypeId" });
            CreateTable(
                "dbo.RegisteredUserTypes",
                c => new
                    {
                        RegisteredUserTypeId = c.Int(nullable: false, identity: true),
                        RegisteredUserTypeName = c.String(),
                    })
                .PrimaryKey(t => t.RegisteredUserTypeId);
            
            AddColumn("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId");
            AddForeignKey("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId", "dbo.RegisteredUserTypes", "RegisteredUserTypeId");
            DropTable("dbo.UserTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        UserTypeName = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId", "dbo.RegisteredUserTypes");
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserType_RegisteredUserTypeId" });
            DropColumn("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId");
            DropTable("dbo.RegisteredUserTypes");
            CreateIndex("dbo.RegisteredUsers", "UserTypeId");
            AddForeignKey("dbo.RegisteredUsers", "UserTypeId", "dbo.UserTypes", "UserTypeId", cascadeDelete: true);
        }
    }
}
