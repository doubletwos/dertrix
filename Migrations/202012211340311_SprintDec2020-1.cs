namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20201 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentSubjects", "OrgId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentSubjects", "OrgId");
        }
    }
}
