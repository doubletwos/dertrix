namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210908 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjectGrades", "Updater_Id", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjectGrades", "Updater_Id");
        }
    }
}
