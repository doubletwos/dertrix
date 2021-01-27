namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20218 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsersGroups", "GroupTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsersGroups", "GroupTypeId");
        }
    }
}
