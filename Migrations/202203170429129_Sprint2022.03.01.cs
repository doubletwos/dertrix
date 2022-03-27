namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220301 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RemovedRegisteredUsers", "Linked_StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RemovedRegisteredUsers", "Linked_StudentId", c => c.Int());
        }
    }
}
