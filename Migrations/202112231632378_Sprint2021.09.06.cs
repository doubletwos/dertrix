namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210906 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjectGrades", "SubjectName", c => c.String());
            AddColumn("dbo.Subjects", "First_Term_Test_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "Second_Term_Test_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "Third_Term_Test_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "First_Term_Exam_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "Second_Term_Exam_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "Third_Term_Exam_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Subjects", "FirstTermSubjectGrade");
            DropColumn("dbo.Subjects", "SecondTermSubjectGrade");
            DropColumn("dbo.Subjects", "ThirdTermSubjectGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "ThirdTermSubjectGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "SecondTermSubjectGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "FirstTermSubjectGrade", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Subjects", "Third_Term_Exam_MaxGrade");
            DropColumn("dbo.Subjects", "Second_Term_Exam_MaxGrade");
            DropColumn("dbo.Subjects", "First_Term_Exam_MaxGrade");
            DropColumn("dbo.Subjects", "Third_Term_Test_MaxGrade");
            DropColumn("dbo.Subjects", "Second_Term_Test_MaxGrade");
            DropColumn("dbo.Subjects", "First_Term_Test_MaxGrade");
            DropColumn("dbo.StudentSubjectGrades", "SubjectName");
        }
    }
}
