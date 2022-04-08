namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220304 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StudentGuardians", "LastLogOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentGuardians", "LastLogOn", c => c.DateTime());
        }
    }
}
