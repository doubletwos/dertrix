namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1903 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.RegisteredUsers", new[] { "SubjectId" });
            AddColumn("dbo.Subjects", "RegisteredUser_RegisteredUserId", c => c.Int());
            CreateIndex("dbo.Subjects", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.Subjects", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Subjects", new[] { "RegisteredUser_RegisteredUserId" });
            DropColumn("dbo.Subjects", "RegisteredUser_RegisteredUserId");
            CreateIndex("dbo.RegisteredUsers", "SubjectId");
            AddForeignKey("dbo.RegisteredUsers", "SubjectId", "dbo.Subjects", "SubjectId");
        }
    }
}
