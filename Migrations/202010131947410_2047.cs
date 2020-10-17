namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2047 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjects", "StudentSelectedSubject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjects", "StudentSelectedSubject");
        }
    }
}
