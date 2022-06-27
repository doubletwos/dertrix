namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220600 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Change_Events_Log",
                c => new
                    {
                        User_Change_Events_LogId = c.Int(nullable: false, identity: true),
                        RegUserId = c.Int(nullable: false),
                        ChangedBy = c.Int(nullable: false),
                        Old_Value = c.String(),
                        New_Value = c.String(),
                        OrgId = c.String(),
                        User_Change_Event_Time = c.DateTime(),
                        User_Change_Events_Types = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.User_Change_Events_LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_Change_Events_Log");
        }
    }
}
