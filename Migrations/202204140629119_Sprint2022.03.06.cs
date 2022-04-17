namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220306 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGuardians", "IsRegistered", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGuardians", "IsRegistered");
        }
    }
}
