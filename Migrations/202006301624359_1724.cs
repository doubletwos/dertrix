namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1724 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "ClassRefNumb", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "ClassRefNumb");
        }
    }
}
