namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1931 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RegisteredUsers", "FirstName");
            DropColumn("dbo.RegisteredUsers", "LastName");
            DropColumn("dbo.RegisteredUsers", "Email");
            DropColumn("dbo.RegisteredUsers", "LoginErrorMsg");
            DropColumn("dbo.RegisteredUsers", "Password");
            DropColumn("dbo.RegisteredUsers", "ConfirmPassword");
            DropColumn("dbo.RegisteredUsers", "Telephone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsers", "Telephone", c => c.String());
            AddColumn("dbo.RegisteredUsers", "ConfirmPassword", c => c.String());
            AddColumn("dbo.RegisteredUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.RegisteredUsers", "LoginErrorMsg", c => c.String());
            AddColumn("dbo.RegisteredUsers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false));
        }
    }
}
