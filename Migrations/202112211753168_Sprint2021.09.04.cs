namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210904 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjectGrades", "FirstTermExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTermExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "ThirdTermExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "FirstTermTestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTermTestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "ThirdTermTestGrade", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.StudentSubjectGrades", "SubjectName");
            DropColumn("dbo.StudentSubjectGrades", "StudentFullName");
            DropColumn("dbo.StudentSubjectGrades", "FirstTermStudentGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTermStudentGrade");
            DropColumn("dbo.StudentSubjectGrades", "ThirdTermStudentGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentSubjectGrades", "ThirdTermStudentGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTermStudentGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "FirstTermStudentGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "StudentFullName", c => c.String());
            AddColumn("dbo.StudentSubjectGrades", "SubjectName", c => c.String());
            DropColumn("dbo.StudentSubjectGrades", "ThirdTermTestGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTermTestGrade");
            DropColumn("dbo.StudentSubjectGrades", "FirstTermTestGrade");
            DropColumn("dbo.StudentSubjectGrades", "ThirdTermExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTermExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "FirstTermExamGrade");
        }
    }
}
