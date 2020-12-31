namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20206 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "SendAsEmail", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "SendAsEmail");
        }
    }
}
