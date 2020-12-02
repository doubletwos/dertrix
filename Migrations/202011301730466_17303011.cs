namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17303011 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegUsersAccessLogs", "OrgId", c => c.Int());
            AlterColumn("dbo.RegUsersAccessLogs", "RegUserId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegUsersAccessLogs", "RegUserId", c => c.String());
            AlterColumn("dbo.RegUsersAccessLogs", "OrgId", c => c.String());
        }
    }
}
