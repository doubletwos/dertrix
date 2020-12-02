namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17393011 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegUsersAccessLogs", "LogInTime", c => c.DateTime());
            AlterColumn("dbo.RegUsersAccessLogs", "LogOutTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegUsersAccessLogs", "LogOutTime", c => c.String());
            AlterColumn("dbo.RegUsersAccessLogs", "LogInTime", c => c.String());
        }
    }
}
