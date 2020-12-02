namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17183011 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegUsersAccessLogs",
                c => new
                    {
                        RegUsersAccessLogId = c.Int(nullable: false, identity: true),
                        OrgId = c.String(),
                        SessionId = c.String(),
                        RegUserId = c.String(),
                        UserFullName = c.String(),
                        LogInTime = c.String(),
                        LogOutTime = c.String(),
                    })
                .PrimaryKey(t => t.RegUsersAccessLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegUsersAccessLogs");
        }
    }
}
