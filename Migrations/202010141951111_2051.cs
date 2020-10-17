namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2051 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjects", "SubjectName", c => c.String());
            AddColumn("dbo.StudentSubjects", "ClassId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjects", "ClassId");
            DropColumn("dbo.StudentSubjects", "SubjectName");
        }
    }
}
