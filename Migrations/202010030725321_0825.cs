namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0825 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "ClassId", "dbo.Classes");
            DropIndex("dbo.RegisteredUsers", new[] { "ClassId" });
            AddColumn("dbo.Classes", "RegisteredUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "registeredUser_RegisteredUserId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "Class_ClassId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "Class_ClassId1", c => c.Int());
            CreateIndex("dbo.Classes", "registeredUser_RegisteredUserId");
            CreateIndex("dbo.RegisteredUsers", "Class_ClassId");
            CreateIndex("dbo.RegisteredUsers", "Class_ClassId1");
            AddForeignKey("dbo.Classes", "registeredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
            AddForeignKey("dbo.RegisteredUsers", "Class_ClassId1", "dbo.Classes", "ClassId");
            AddForeignKey("dbo.RegisteredUsers", "Class_ClassId", "dbo.Classes", "ClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "Class_ClassId", "dbo.Classes");
            DropForeignKey("dbo.RegisteredUsers", "Class_ClassId1", "dbo.Classes");
            DropForeignKey("dbo.Classes", "registeredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.RegisteredUsers", new[] { "Class_ClassId1" });
            DropIndex("dbo.RegisteredUsers", new[] { "Class_ClassId" });
            DropIndex("dbo.Classes", new[] { "registeredUser_RegisteredUserId" });
            DropColumn("dbo.RegisteredUsers", "Class_ClassId1");
            DropColumn("dbo.RegisteredUsers", "Class_ClassId");
            DropColumn("dbo.Classes", "registeredUser_RegisteredUserId");
            DropColumn("dbo.Classes", "RegisteredUserId");
            CreateIndex("dbo.RegisteredUsers", "ClassId");
            AddForeignKey("dbo.RegisteredUsers", "ClassId", "dbo.Classes", "ClassId");
        }
    }
}
