namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20120755 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "SubjectOrgId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "SubjectOrgId");
        }
    }
}
