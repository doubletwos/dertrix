namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20202 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "ClassRef", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "ClassRef");
        }
    }
}
