namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1933 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Subjects", new[] { "RegisteredUser_RegisteredUserId" });
            DropColumn("dbo.Subjects", "RegisteredUser_RegisteredUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "RegisteredUser_RegisteredUserId", c => c.Int());
            CreateIndex("dbo.Subjects", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.Subjects", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
    }
}
