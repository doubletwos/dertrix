namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintFeb20212 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "TitleId", c => c.Int());
            CreateIndex("dbo.Classes", "TitleId");
            AddForeignKey("dbo.Classes", "TitleId", "dbo.Titles", "TitleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "TitleId", "dbo.Titles");
            DropIndex("dbo.Classes", new[] { "TitleId" });
            DropColumn("dbo.Classes", "TitleId");
        }
    }
}
