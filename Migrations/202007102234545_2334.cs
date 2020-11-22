namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2334 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false));
        }
    }
}
