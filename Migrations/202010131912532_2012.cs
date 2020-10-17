namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2012 : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[StudentSubjects] DROP CONSTRAINT [FK_dbo.StudentSubjects_dbo.Subjects_SubjectId]");
        }
        
        public override void Down()
        {
        }
    }
}
