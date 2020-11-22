namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2219 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId", "dbo.RegisteredUserTypes");
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserType_RegisteredUserTypeId" });
            RenameColumn(table: "dbo.RegisteredUsers", name: "RegisteredUserType_RegisteredUserTypeId", newName: "RegisteredUserTypeId");
            AlterColumn("dbo.RegisteredUsers", "RegisteredUserTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.RegisteredUsers", "RegisteredUserTypeId");
            AddForeignKey("dbo.RegisteredUsers", "RegisteredUserTypeId", "dbo.RegisteredUserTypes", "RegisteredUserTypeId", cascadeDelete: true);
            DropColumn("dbo.RegisteredUsers", "UserTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsers", "UserTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserTypeId", "dbo.RegisteredUserTypes");
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserTypeId" });
            AlterColumn("dbo.RegisteredUsers", "RegisteredUserTypeId", c => c.Int());
            RenameColumn(table: "dbo.RegisteredUsers", name: "RegisteredUserTypeId", newName: "RegisteredUserType_RegisteredUserTypeId");
            CreateIndex("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId");
            AddForeignKey("dbo.RegisteredUsers", "RegisteredUserType_RegisteredUserTypeId", "dbo.RegisteredUserTypes", "RegisteredUserTypeId");
        }
    }
}
