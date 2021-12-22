namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210902 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentSubjects", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.StudentSubjects", new[] { "RegisteredUserId" });
            DropIndex("dbo.StudentSubjects", new[] { "SubjectId" });
            CreateTable(
                "dbo.StudentSubjectGrades",
                c => new
                    {
                        StudentSubjectGradeId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        SubjectName = c.String(),
                        StudentFullName = c.String(),
                        ClassId = c.Int(),
                        ClassRef = c.Int(),
                        OrgId = c.Int(),
                        FirstTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                        SecondTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                        ThirdTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StudentSubjectGradeId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.SubjectId);
            
            DropTable("dbo.StudentSubjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        StudentSubjectId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        SubjectName = c.String(),
                        StudentFullName = c.String(),
                        ClassId = c.Int(),
                        ClassRef = c.Int(),
                        OrgId = c.Int(),
                        FirstTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                        SecondTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                        ThirdTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StudentSubjectId);
            
            DropForeignKey("dbo.StudentSubjectGrades", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjectGrades", "RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.StudentSubjectGrades", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjectGrades", new[] { "RegisteredUserId" });
            DropTable("dbo.StudentSubjectGrades");
            CreateIndex("dbo.StudentSubjects", "SubjectId");
            CreateIndex("dbo.StudentSubjects", "RegisteredUserId");
            AddForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
            AddForeignKey("dbo.StudentSubjects", "RegisteredUserId", "dbo.RegisteredUsers", "RegisteredUserId", cascadeDelete: true);
        }
    }
}
