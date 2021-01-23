namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20212 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "TitleId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "TitleId");
            AddForeignKey("dbo.RegisteredUsers", "TitleId", "dbo.Titles", "TitleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "TitleId", "dbo.Titles");
            DropIndex("dbo.RegisteredUsers", new[] { "TitleId" });
            DropColumn("dbo.RegisteredUsers", "TitleId");
        }
    }
}
