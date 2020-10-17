namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1606 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StudentSubjects", "StudentSelectedSubject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentSubjects", "StudentSelectedSubject", c => c.String());
        }
    }
}
