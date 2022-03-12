namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220300 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RemovedRegisteredUsers", "Linked_StudentId", c => c.Int());
            AddColumn("dbo.RemovedRegisteredUsers", "RelationshipId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RemovedRegisteredUsers", "RelationshipId");
            DropColumn("dbo.RemovedRegisteredUsers", "Linked_StudentId");
        }
    }
}
