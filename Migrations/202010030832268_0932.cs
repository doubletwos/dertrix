namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0932 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "ClassTeacher_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Classes", new[] { "ClassTeacher_RegisteredUserId" });
            DropIndex("dbo.RegisteredUsers", new[] { "Class_ClassId1" });
            //DropColumn("dbo.RegisteredUsers", "ClassId");
            //DropColumn("dbo.RegisteredUsers", "ClassId");
            //RenameColumn(table: "dbo.RegisteredUsers", name: "Class_ClassId1", newName: "ClassId");
            //RenameColumn(table: "dbo.RegisteredUsers", name: "Class_ClassId", newName: "ClassId");
            RenameIndex(table: "dbo.RegisteredUsers", name: "IX_Class_ClassId", newName: "IX_ClassId");
            DropColumn("dbo.Classes", "RegisteredUserId");
            DropColumn("dbo.Classes", "ClassTeacher_RegisteredUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "ClassTeacher_RegisteredUserId", c => c.Int());
            AddColumn("dbo.Classes", "RegisteredUserId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.RegisteredUsers", name: "IX_ClassId", newName: "IX_Class_ClassId");
            RenameColumn(table: "dbo.RegisteredUsers", name: "ClassId", newName: "Class_ClassId");
            RenameColumn(table: "dbo.RegisteredUsers", name: "ClassId", newName: "Class_ClassId1");
            AddColumn("dbo.RegisteredUsers", "ClassId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "ClassId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "Class_ClassId1");
            CreateIndex("dbo.Classes", "ClassTeacher_RegisteredUserId");
            AddForeignKey("dbo.Classes", "ClassTeacher_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
    }
}
