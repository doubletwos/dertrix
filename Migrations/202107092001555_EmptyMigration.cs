namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrgGroups", "OrgEvent_OrgEventId", "dbo.OrgEvents");
            DropIndex("dbo.OrgGroups", new[] { "OrgEvent_OrgEventId" });
            DropColumn("dbo.OrgGroups", "OrgEvent_OrgEventId");
            DropTable("dbo.OrgEvents");
            DropTable("dbo.OrgImportantDates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrgImportantDates",
                c => new
                    {
                        OrgImportantDateId = c.Int(nullable: false, identity: true),
                        ImportantDateName = c.String(),
                        OrgId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatorName = c.String(),
                        FromImportanttDate = c.DateTime(),
                        ToImportanttDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrgImportantDateId);
            
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
        }
    }
}
