namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20219 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false, maxLength: 12));
        }
    }
}
