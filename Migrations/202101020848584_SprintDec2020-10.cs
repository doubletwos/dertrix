namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec202010 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUsers", "SelectedOrg", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUsers", "SelectedOrg", c => c.Int(nullable: false));
        }
    }
}
