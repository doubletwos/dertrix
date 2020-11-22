namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0849 : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[Subjects] DROP CONSTRAINT [FK_dbo.Subjects_dbo.ClassTeachers_ClassTeacherId]");

        }

        public override void Down()
        {
        }
    }
}
