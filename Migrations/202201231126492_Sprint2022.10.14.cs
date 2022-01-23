namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RemovedRegisteredUsers", "LastLogOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RemovedRegisteredUsers", "LastLogOn");
        }
    }
}
