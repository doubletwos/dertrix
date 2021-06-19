namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210702 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarCategories",
                c => new
                    {
                        CalendarCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CalendarCategoryId);
            
            CreateTable(
                "dbo.OrgSchCalendars",
                c => new
                    {
                        OrgSchCalendarId = c.Int(nullable: false, identity: true),
                        CalendarCategoryId = c.Int(nullable: false),
                        OrgId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatorId = c.Int(nullable: false),
                        CreatorFullName = c.String(),
                        CreationDate = c.DateTime(),
                        IsRecurring = c.Boolean(),
                        Frequency = c.Int(),
                        SendAsEmail = c.Boolean(),
                    })
                .PrimaryKey(t => t.OrgSchCalendarId)
                .ForeignKey("dbo.CalendarCategories", t => t.CalendarCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .Index(t => t.CalendarCategoryId)
                .Index(t => t.OrgId);
            
            AddColumn("dbo.OrgGroups", "OrgSchCalendar_OrgSchCalendarId", c => c.Int());
            CreateIndex("dbo.OrgGroups", "OrgSchCalendar_OrgSchCalendarId");
            AddForeignKey("dbo.OrgGroups", "OrgSchCalendar_OrgSchCalendarId", "dbo.OrgSchCalendars", "OrgSchCalendarId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrgGroups", "OrgSchCalendar_OrgSchCalendarId", "dbo.OrgSchCalendars");
            DropForeignKey("dbo.OrgSchCalendars", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.OrgSchCalendars", "CalendarCategoryId", "dbo.CalendarCategories");
            DropIndex("dbo.OrgSchCalendars", new[] { "OrgId" });
            DropIndex("dbo.OrgSchCalendars", new[] { "CalendarCategoryId" });
            DropIndex("dbo.OrgGroups", new[] { "OrgSchCalendar_OrgSchCalendarId" });
            DropColumn("dbo.OrgGroups", "OrgSchCalendar_OrgSchCalendarId");
            DropTable("dbo.OrgSchCalendars");
            DropTable("dbo.CalendarCategories");
        }
    }
}
