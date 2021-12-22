namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210905 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjectGrades", "FirstTerm_ExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTerm_ExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "ThirdTerm_ExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "FirstTerm_TestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTerm_TestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "ThirdTerm_TestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "Last_updated_date", c => c.DateTime());
            AddColumn("dbo.StudentSubjectGrades", "Created_date", c => c.DateTime());
            DropColumn("dbo.StudentSubjectGrades", "FirstTermExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTermExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "ThirdTermExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "FirstTermTestGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTermTestGrade");
            DropColumn("dbo.StudentSubjectGrades", "ThirdTermTestGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentSubjectGrades", "ThirdTermTestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTermTestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "FirstTermTestGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "ThirdTermExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "SecondTermExamGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "FirstTermExamGrade", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.StudentSubjectGrades", "Created_date");
            DropColumn("dbo.StudentSubjectGrades", "Last_updated_date");
            DropColumn("dbo.StudentSubjectGrades", "ThirdTerm_TestGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTerm_TestGrade");
            DropColumn("dbo.StudentSubjectGrades", "FirstTerm_TestGrade");
            DropColumn("dbo.StudentSubjectGrades", "ThirdTerm_ExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "SecondTerm_ExamGrade");
            DropColumn("dbo.StudentSubjectGrades", "FirstTerm_ExamGrade");
        }
    }
}
