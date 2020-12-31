namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20205 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsersGroups", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsersGroups", "Email");
        }
    }
}
