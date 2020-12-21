namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec2020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjects", "ClassRef", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjects", "ClassRef");
        }
    }
}
