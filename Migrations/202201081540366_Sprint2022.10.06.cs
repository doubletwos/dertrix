namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students_Grades_Log", "StudentClassChangeType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students_Grades_Log", "StudentClassChangeType");
        }
    }
}
