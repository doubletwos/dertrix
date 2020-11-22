namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2037 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RegisteredUsers", name: "Class_ClassId", newName: "ClassId");
            RenameIndex(table: "dbo.RegisteredUsers", name: "IX_Class_ClassId", newName: "IX_ClassId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RegisteredUsers", name: "IX_ClassId", newName: "IX_Class_ClassId");
            RenameColumn(table: "dbo.RegisteredUsers", name: "ClassId", newName: "Class_ClassId");
        }
    }
}
