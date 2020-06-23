namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2217 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId", c => c.Int());
            CreateIndex("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Orgs", new[] { "RegisteredUser_RegisteredUserId" });
            DropColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId");
        }
    }
}
