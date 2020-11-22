namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2057 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "LoginErrorMsg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "LoginErrorMsg");
        }
    }
}
