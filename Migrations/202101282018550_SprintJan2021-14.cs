namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan202114 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGuardians", "TitleId", c => c.Int());
            AddColumn("dbo.StudentGuardians", "RelationshipId", c => c.Int());
            CreateIndex("dbo.StudentGuardians", "TitleId");
            CreateIndex("dbo.StudentGuardians", "RelationshipId");
            AddForeignKey("dbo.StudentGuardians", "RelationshipId", "dbo.Relationships", "RelationshipId");
            AddForeignKey("dbo.StudentGuardians", "TitleId", "dbo.Titles", "TitleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGuardians", "TitleId", "dbo.Titles");
            DropForeignKey("dbo.StudentGuardians", "RelationshipId", "dbo.Relationships");
            DropIndex("dbo.StudentGuardians", new[] { "RelationshipId" });
            DropIndex("dbo.StudentGuardians", new[] { "TitleId" });
            DropColumn("dbo.StudentGuardians", "RelationshipId");
            DropColumn("dbo.StudentGuardians", "TitleId");
        }
    }
}
