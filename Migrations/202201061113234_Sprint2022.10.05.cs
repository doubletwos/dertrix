namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221005 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students_Grades_Log",
                c => new
                    {
                        Students_Grades_LogId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        ClassRef = c.Int(nullable: false),
                        OrgId = c.Int(nullable: false),
                        FirstTerm_ExamGrade = c.Decimal(precision: 18, scale: 2),
                        SecondTerm_ExamGrade = c.Decimal(precision: 18, scale: 2),
                        ThirdTerm_ExamGrade = c.Decimal(precision: 18, scale: 2),
                        FirstTerm_TestGrade = c.Decimal(precision: 18, scale: 2),
                        SecondTerm_TestGrade = c.Decimal(precision: 18, scale: 2),
                        ThirdTerm_TestGrade = c.Decimal(precision: 18, scale: 2),
                        Last_updated_date = c.DateTime(),
                        First_Term_Test_MaxGrade = c.Decimal(precision: 18, scale: 2),
                        Second_Term_Test_MaxGrade = c.Decimal(precision: 18, scale: 2),
                        Third_Term_Test_MaxGrade = c.Decimal(precision: 18, scale: 2),
                        First_Term_Exam_MaxGrade = c.Decimal(precision: 18, scale: 2),
                        Second_Term_Exam_MaxGrade = c.Decimal(precision: 18, scale: 2),
                        Third_Term_Exam_MaxGrade = c.Decimal(precision: 18, scale: 2),
                        ClassTeacherId = c.Int(),
                        Created_date = c.DateTime(),
                        Updater_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Students_Grades_LogId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: false)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: false)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: false)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.SubjectId)
                .Index(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students_Grades_Log", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Students_Grades_Log", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Students_Grades_Log", "ClassId", "dbo.Classes");
            DropIndex("dbo.Students_Grades_Log", new[] { "ClassId" });
            DropIndex("dbo.Students_Grades_Log", new[] { "SubjectId" });
            DropIndex("dbo.Students_Grades_Log", new[] { "RegisteredUserId" });
            DropTable("dbo.Students_Grades_Log");
        }
    }
}
