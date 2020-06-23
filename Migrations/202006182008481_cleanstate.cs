namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanstate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.Orgs", new[] { "RegisteredUser_RegisteredUserId" });
            AddColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.RegisteredUsers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.RegisteredUsers", "LoginErrorMsg", c => c.String());
            AddColumn("dbo.RegisteredUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.RegisteredUsers", "ConfirmPassword", c => c.String());
            AddColumn("dbo.RegisteredUsers", "Telephone", c => c.String());
            DropColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orgs", "RegisteredUser_RegisteredUserId", c => c.Int());
            DropColumn("dbo.RegisteredUsers", "Telephone");
            DropColumn("dbo.RegisteredUsers", "ConfirmPassword");
            DropColumn("dbo.RegisteredUsers", "Password");
            DropColumn("dbo.RegisteredUsers", "LoginErrorMsg");
            DropColumn("dbo.RegisteredUsers", "Email");
            DropColumn("dbo.RegisteredUsers", "LastName");
            DropColumn("dbo.RegisteredUsers", "FirstName");
            CreateIndex("dbo.Orgs", "RegisteredUser_RegisteredUserId");
            AddForeignKey("dbo.Orgs", "RegisteredUser_RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId");
        }
    }
}
