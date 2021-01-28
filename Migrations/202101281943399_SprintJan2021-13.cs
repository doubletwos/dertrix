namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan202113 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "RelationshipId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "RelationshipId");
            AddForeignKey("dbo.RegisteredUsers", "RelationshipId", "dbo.Relationships", "RelationshipId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "RelationshipId", "dbo.Relationships");
            DropIndex("dbo.RegisteredUsers", new[] { "RelationshipId" });
            DropColumn("dbo.RegisteredUsers", "RelationshipId");
        }
    }
}
