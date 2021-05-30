namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210601 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Org_Events_Log",
                c => new
                    {
                        Org_Events_LogId = c.Int(nullable: false, identity: true),
                        Org_Event_TypeId = c.Int(nullable: false),
                        Org_Event_Name = c.String(),
                        Org_Event_SubjectId = c.String(),
                        Org_Event_SubjectName = c.String(),
                        Org_Event_TriggeredbyId = c.String(),
                        Org_Event_TriggeredbyName = c.String(),
                        Org_Event_Time = c.DateTime(),
                    })
                .PrimaryKey(t => t.Org_Events_LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Org_Events_Log");
        }
    }
}
