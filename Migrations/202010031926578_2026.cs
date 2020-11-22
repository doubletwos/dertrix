namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2026 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "ClassTeacherFullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "ClassTeacherFullName");
        }
    }
}
