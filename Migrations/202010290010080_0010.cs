namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0010 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RegisteredUsersGroups", "GroupTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsersGroups", "GroupTypeId", c => c.Int(nullable: false));
        }
    }
}
