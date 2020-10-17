namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2308 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Subject_SubjectId", "dbo.Subjects");
            DropIndex("dbo.Subjects", new[] { "Subject_SubjectId" });
            DropColumn("dbo.Subjects", "Subject_SubjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Subject_SubjectId", c => c.Int());
            CreateIndex("dbo.Subjects", "Subject_SubjectId");
            AddForeignKey("dbo.Subjects", "Subject_SubjectId", "dbo.Subjects", "SubjectId");
        }
    }
}
