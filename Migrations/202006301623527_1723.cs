namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1723 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Classes", "ClassRefNumb");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "ClassRefNumb", c => c.Int(nullable: false));
        }
    }
}
