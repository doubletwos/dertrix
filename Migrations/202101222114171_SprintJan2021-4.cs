namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20214 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "TitleId", c => c.Int());
            CreateIndex("dbo.RegisteredUserOrganisations", "TitleId");
            AddForeignKey("dbo.RegisteredUserOrganisations", "TitleId", "dbo.Titles", "TitleId");
            DropColumn("dbo.RegisteredUserOrganisations", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUserOrganisations", "Title", c => c.String());
            DropForeignKey("dbo.RegisteredUserOrganisations", "TitleId", "dbo.Titles");
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "TitleId" });
            DropColumn("dbo.RegisteredUserOrganisations", "TitleId");
        }
    }
}
