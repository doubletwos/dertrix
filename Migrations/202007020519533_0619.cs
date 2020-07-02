namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0619 : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[RegisteredUsers] DROP CONSTRAINT [FK_dbo.RegisteredUsers_dbo.Classes_Class_ClassId]");
        }
        
        public override void Down()
        {
        }
    }
}
