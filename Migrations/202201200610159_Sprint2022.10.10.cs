namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221010 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "First_Term_Test_MaxGrade", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "First_Term_Test_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
