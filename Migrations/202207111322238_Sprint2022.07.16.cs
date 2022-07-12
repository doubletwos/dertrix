namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220716 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgClassPeriods", "SubjectId", c => c.Int());
            CreateIndex("dbo.OrgClassPeriods", "SubjectId");
            AddForeignKey("dbo.OrgClassPeriods", "SubjectId", "dbo.Subjects", "SubjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrgClassPeriods", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.OrgClassPeriods", new[] { "SubjectId" });
            DropColumn("dbo.OrgClassPeriods", "SubjectId");
        }
    }
}
