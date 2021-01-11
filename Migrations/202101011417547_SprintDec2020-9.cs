namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20209 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "TempIntHolder", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "TempIntHolder");
        }
    }
}
