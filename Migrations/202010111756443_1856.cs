namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1856 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "SubjectId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "SubjectId");
            AddForeignKey("dbo.RegisteredUsers", "SubjectId", "dbo.Subjects", "SubjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.RegisteredUsers", new[] { "SubjectId" });
            DropColumn("dbo.RegisteredUsers", "SubjectId");
        }
    }
}
