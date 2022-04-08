namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220305 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "InviteSentDate", c => c.DateTime());
            AddColumn("dbo.RegisteredUsers", "CountOfInvite", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "IsRegistered", c => c.Boolean());
            AddColumn("dbo.RegisteredUsers", "RegisteredDate", c => c.DateTime());
            DropColumn("dbo.StudentGuardians", "IsRegistered");
            DropColumn("dbo.StudentGuardians", "RegisteredDate");
            DropColumn("dbo.StudentGuardians", "InviteSentDate");
            DropColumn("dbo.StudentGuardians", "CountOfInvite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentGuardians", "CountOfInvite", c => c.Int());
            AddColumn("dbo.StudentGuardians", "InviteSentDate", c => c.DateTime());
            AddColumn("dbo.StudentGuardians", "RegisteredDate", c => c.DateTime());
            AddColumn("dbo.StudentGuardians", "IsRegistered", c => c.Boolean());
            DropColumn("dbo.RegisteredUsers", "RegisteredDate");
            DropColumn("dbo.RegisteredUsers", "IsRegistered");
            DropColumn("dbo.RegisteredUsers", "CountOfInvite");
            DropColumn("dbo.RegisteredUsers", "InviteSentDate");
        }
    }
}
