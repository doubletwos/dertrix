namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan202111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Relationships", "RelationshipName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Relationships", "RelationshipName");
        }
    }
}
