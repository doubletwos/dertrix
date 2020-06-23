namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2044 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.RegisteredUsers", "ConfirmPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "ConfirmPassword");
            DropColumn("dbo.RegisteredUsers", "Password");
        }
    }
}
