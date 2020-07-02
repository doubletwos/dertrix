namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1414 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "Class_ClassId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "Class_ClassId");
            AddForeignKey("dbo.RegisteredUsers", "Class_ClassId", "dbo.Classes", "ClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "Class_ClassId", "dbo.Classes");
            DropIndex("dbo.RegisteredUsers", new[] { "Class_ClassId" });
            DropColumn("dbo.RegisteredUsers", "Class_ClassId");
        }
    }
}
