namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210803 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsersGroups", "LinkedStudentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsersGroups", "LinkedStudentId");
        }
    }
}
