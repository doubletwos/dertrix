namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1353 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "FirstTermSubjectGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "SecondTermSubjectGrade", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subjects", "ThirdTermSubjectGrade", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "ThirdTermSubjectGrade");
            DropColumn("dbo.Subjects", "SecondTermSubjectGrade");
            DropColumn("dbo.Subjects", "FirstTermSubjectGrade");
        }
    }
}
