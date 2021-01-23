namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20211 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titles", "TitleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titles", "TitleName");
        }
    }
}
