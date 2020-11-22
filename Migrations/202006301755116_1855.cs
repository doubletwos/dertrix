namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1855 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "ClassRefNumb", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classes", "ClassRefNumb", c => c.Int(nullable: false));
        }
    }
}
