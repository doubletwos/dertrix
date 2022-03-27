namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220302 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGuardians", "IsRegistered", c => c.Boolean());
            AddColumn("dbo.StudentGuardians", "RegisteredDate", c => c.DateTime());
            AddColumn("dbo.StudentGuardians", "LastLogOn", c => c.DateTime());
            AddColumn("dbo.StudentGuardians", "InviteSentDate", c => c.DateTime());
            AddColumn("dbo.StudentGuardians", "CountOfInvite", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGuardians", "CountOfInvite");
            DropColumn("dbo.StudentGuardians", "InviteSentDate");
            DropColumn("dbo.StudentGuardians", "LastLogOn");
            DropColumn("dbo.StudentGuardians", "RegisteredDate");
            DropColumn("dbo.StudentGuardians", "IsRegistered");
        }
    }
}
