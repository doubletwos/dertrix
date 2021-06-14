namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210604 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "PgCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "PgCount");
        }
    }
}
