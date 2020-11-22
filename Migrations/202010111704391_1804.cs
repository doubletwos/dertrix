namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1804 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Subjects", name: "RegisteredUsers_RegisteredUserId", newName: "RegisteredUser_RegisteredUserId");
            RenameIndex(table: "dbo.Subjects", name: "IX_RegisteredUsers_RegisteredUserId", newName: "IX_RegisteredUser_RegisteredUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Subjects", name: "IX_RegisteredUser_RegisteredUserId", newName: "IX_RegisteredUsers_RegisteredUserId");
            RenameColumn(table: "dbo.Subjects", name: "RegisteredUser_RegisteredUserId", newName: "RegisteredUsers_RegisteredUserId");
        }
    }
}
