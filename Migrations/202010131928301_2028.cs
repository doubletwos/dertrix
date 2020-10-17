namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2028 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StudentSubjects", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentSubjects", "test", c => c.String());
        }
    }
}
