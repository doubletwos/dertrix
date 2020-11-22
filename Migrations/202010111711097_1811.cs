namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1811 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        StudentSubjectId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentSubjectId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.StudentSubjects", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjects", new[] { "RegisteredUserId" });
            DropTable("dbo.StudentSubjects");
        }
    }
}
