namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220500 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Isarchived", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Isarchived");
        }
    }
}
