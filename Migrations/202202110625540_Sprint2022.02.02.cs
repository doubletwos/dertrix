namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220202 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjectGrades", "Subject_Min_Passmark", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StudentSubjectGrades", "Subject_Max_Passmark", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjectGrades", "Subject_Max_Passmark");
            DropColumn("dbo.StudentSubjectGrades", "Subject_Min_Passmark");
        }
    }
}
