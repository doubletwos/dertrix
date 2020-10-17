namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1840 : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[Subjects] DROP CONSTRAINT [FK_dbo.Subjects_dbo.RegisteredUsers_RegisteredUsers_RegisteredUserId]");
        }
        
        public override void Down()
        {
     
        }
    }
}
