namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2244 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Subject_SubjectId", c => c.Int());
            CreateIndex("dbo.Subjects", "Subject_SubjectId");
            AddForeignKey("dbo.Subjects", "Subject_SubjectId", "dbo.Subjects", "SubjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Subject_SubjectId", "dbo.Subjects");
            DropIndex("dbo.Subjects", new[] { "Subject_SubjectId" });
            DropColumn("dbo.Subjects", "Subject_SubjectId");
        }
    }
}
