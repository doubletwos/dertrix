namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20207 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsersGroups", "RegUserOrgId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsersGroups", "RegUserOrgId");
        }
    }
}
