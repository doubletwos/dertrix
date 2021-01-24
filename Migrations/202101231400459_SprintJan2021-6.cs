namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20216 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgEvents",
                c => new
                    {
                        OrgEventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        OrgId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatorName = c.String(),
                        EventDescription = c.String(),
                        EventDate = c.DateTime(),
                        SendAsEmail = c.Boolean(),
                    })
                .PrimaryKey(t => t.OrgEventId);
            
            AddColumn("dbo.OrgGroups", "OrgEvent_OrgEventId", c => c.Int());
            CreateIndex("dbo.OrgGroups", "OrgEvent_OrgEventId");
            AddForeignKey("dbo.OrgGroups", "OrgEvent_OrgEventId", "dbo.OrgEvents", "OrgEventId");
            DropColumn("dbo.OrgImportantDates", "DateisPast");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgImportantDates", "DateisPast", c => c.Boolean());
            DropForeignKey("dbo.OrgGroups", "OrgEvent_OrgEventId", "dbo.OrgEvents");
            DropIndex("dbo.OrgGroups", new[] { "OrgEvent_OrgEventId" });
            DropColumn("dbo.OrgGroups", "OrgEvent_OrgEventId");
            DropTable("dbo.OrgEvents");
        }
    }
}
