namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1731 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "SubjectName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "SubjectName", c => c.String());
        }
    }
}
