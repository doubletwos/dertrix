namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220303 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "InviteKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "InviteKey");
        }
    }
}
