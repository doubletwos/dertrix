namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1357 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjects", "FirstTermStudentGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjects", "SecondTermStudentGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjects", "ThirdTermStudentGrade", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjects", "ThirdTermStudentGrade");
            DropColumn("dbo.StudentSubjects", "SecondTermStudentGrade");
            DropColumn("dbo.StudentSubjects", "FirstTermStudentGrade");
        }
    }
}
