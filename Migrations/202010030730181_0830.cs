namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0830 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Classes", name: "registeredUser_RegisteredUserId", newName: "ClassTeacher_RegisteredUserId");
            RenameIndex(table: "dbo.Classes", name: "IX_registeredUser_RegisteredUserId", newName: "IX_ClassTeacher_RegisteredUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Classes", name: "IX_ClassTeacher_RegisteredUserId", newName: "IX_registeredUser_RegisteredUserId");
            RenameColumn(table: "dbo.Classes", name: "ClassTeacher_RegisteredUserId", newName: "registeredUser_RegisteredUserId");
        }
    }
}
